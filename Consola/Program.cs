using Consola;
using Data;
using System.Data;

//string rutaArchivo = @"C:\curso.NET\separado.csv";
Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
string archivoConfiguracion = Path.Combine(Environment.CurrentDirectory,".Env");
var configuracion = new Configuracion();
var conf = configuracion.ObtenerConfiguracion(archivoConfiguracion);


var conexion = new Conexion(conf["Servidor"], conf["BaseDatos"]);
var manejadorArchivo = new ManejadorArchivo();

var usuarios = conexion.ObtenerUsuariosSinSincronizar();
var nombreArchivo = $"{Guid.NewGuid()}.csv";

foreach (DataRow usuario in usuarios.Tables[0].Rows)
{
    //var ss = Convert.ToInt32(usuario["id"].ToString());
    //Console.WriteLine(cadena);
    try
    {
        if (manejadorArchivo.GuardarEnCSV(usuario, conf["﻿RutaArchivo"], nombreArchivo))
        {
            conexion.ActualizarSincronizado(int.Parse(usuario["id"].ToString()));
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

/*var destinatarios = new List<string>
{
    "marianausugamontoya12344@gmail.com",
};*/
var destinatarios = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "des.txt"));

var listaArchivos = new List<string>
{
    Path.Combine(conf["﻿RutaArchivo"], nombreArchivo)
};

var email = Correo.CrearMensaje(destinatarios.ToList(), $"prueba correo {DateTime.Now.ToString("yyyyMMdddhh:mm:ss")}", "Hola esto es una prueba", false, "usugamontoya1@outlook.es", "Mariana usuga");
email.AdjuntarArchivos(listaArchivos);
email.EnviarMensaje(conf["UsuarioEmail"], conf["ClaveEmail"],conf["HostEmail"], int.Parse(conf["PuertoEmail"]), bool.Parse(conf["UsaSSL"]));
//File.Delete(Path.Combine(conf["﻿RutaArchivo"], nombreArchivo));

Console.WriteLine("Fin lectura");