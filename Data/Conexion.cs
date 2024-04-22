using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace Data
{
    public class Conexion(string servidor, string bd)
    {
        private readonly string cadena = $"Server={servidor};Database={bd};Integrated Security=True; TrustServerCertificate=True";

        /*private string cadena;
        public Conexion(string servidor, string bd)
        {
            cadena = $"Server={servidor};Database={bd};TrustServerCertificate=True";
        }*/

        public DataSet ObtenerUsuariosSinSincronizar()
        {
            DataSet dt = new DataSet();
            using SqlConnection conn = new SqlConnection(cadena);
            var query = "ConsultarUsuariosNoSincronizados";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var adaptador = new SqlDataAdapter(cmd);
                adaptador.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Se ha generado un error al conectar o ejecutar la consulta, {ex.Message}");
            }


        }
                public bool ActualizarSincronizado(int idUsuario)
                {
                    using SqlConnection conn = new SqlConnection(cadena);
                    var query = "GuardarUsuarioSincronizado";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UsuarioId",idUsuario);
                    try
                    {
                        conn.Open();
                      var resultado = cmd.ExecuteNonQuery();
                     if (resultado > 0)
                     {
                        return true;
                      }
                       return false;
                        //return cmd.ExecuteNonQuery() > 0;

                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Se ha generado un error al guardar sincronizado, {ex.Message}");
                    }
                }


            }

        }
