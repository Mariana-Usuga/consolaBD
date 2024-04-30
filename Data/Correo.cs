using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data
{
    public static class Correo
    {
        public static MailMessage CrearMensaje(List<string> destinatarios, string asunto, string cuerpo, bool esHtml, string usuario, string usuarioAMostrar

            )
        {
            MailMessage mensaje = new MailMessage();
            foreach(string destinario in destinatarios)
            {
                if (EmailValido(destinario))
                {
                    mensaje.CC.Add(destinario);
                }else
                {
                    Console.WriteLine($"El correo {destinario} no es un email valido");
                }
                //mesaje.CC.Add(destinario);
            }
            mensaje.From = new MailAddress(usuario, usuarioAMostrar, Encoding.UTF8);
            mensaje.Subject = asunto;
            mensaje.Body = cuerpo;
            mensaje.BodyEncoding = Encoding.UTF8;
            mensaje.Priority = MailPriority.Normal;
            mensaje.IsBodyHtml = esHtml;
            return mensaje;

        }

        public static void AdjuntarArchivos(this MailMessage mensaje, List<string> archivos)
        {
            if (mensaje != null)
            {
                if (archivos.Count != 0)
                {
                    foreach (string archivo in archivos)
                    {
                        if (File.Exists(archivo))
                        {
                            Attachment adjunto = new Attachment(archivo);
                            mensaje.Attachments.Add(adjunto);
                        }
                    }
                }
            }
        }

        public static void EnviarMensaje(this MailMessage mensaje, string usuario, string clave, string host, int puerto, bool usaSSL)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = host;
            smtp.Port = puerto;
            smtp.EnableSsl = usaSSL;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(usuario, clave);
            //smtp.Timeout = 2000;

            try
            {
                smtp.Send(mensaje);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar email, {ex.Message}");
            }
        }

        private static bool EmailValido(string email)
        {
            Regex regex = new Regex(@"^([\w\.-]+)@([\w\.-]+((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }



    }
}
