using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consola
{
    public class Configuracion
    {
        public static Dictionary<string, string> Conf = new Dictionary<string, string>();

        public Dictionary<string, string> ObtenerConfiguracion(string rutaAchivoConfiguracion)
        {
            try
            {
                if (string.IsNullOrEmpty(rutaAchivoConfiguracion)) throw new Exception("La ruta del archivo de configuracion no puede ser null o vacio");
                var confi = File.ReadAllLines(rutaAchivoConfiguracion);
                foreach (var linea in confi)
                {
                    var lineaConfi = linea.Split("=");
                    Conf.Add(lineaConfi[0], lineaConfi[1]);
                }
                return Conf;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer archivo de configuracion, {ex.Message}");
                return null;
            }
        }

    }
}
