using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ManejadorArchivo
    {
        public bool GuardarEnCSV(DataRow row, string rutaArchivo, string nombreArchivo)
        {
            rutaArchivo = Path.Combine(rutaArchivo, nombreArchivo);
            if (string.IsNullOrEmpty(rutaArchivo)) { throw new Exception("La ruta no puede ser vacia"); }
            try
            {
                var cadena = $"{row["id"]},{row["nombre"]},{row["apellido"]},{row["email"]},{row["genero"]},{row["usuario"]},{row["activo"]}";
                var directorio = Path.GetDirectoryName(rutaArchivo);
                if (!Directory.Exists(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }

                using StreamWriter writer = new StreamWriter(rutaArchivo, true);
                writer.WriteLine(cadena);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se ha generado un error al guardar en el archivo [{rutaArchivo}], {ex.Message}");
                return false;
            }
        }
    }
}

