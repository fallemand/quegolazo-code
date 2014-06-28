using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.Security;


namespace Utils
{
    public class GestorMails
    {
        /// <summary>
        /// metodo para mandar mails
        /// autor: Flor
        /// </summary>
        /// <param name="destinatario"></param>
        /// <param name="asunto"></param>
        /// <param name="cuerpo"></param>
        public void mandarMail(string destinatario, string asunto, string cuerpo)
        {
            MailMessage msg;
            string ActivationUrl = string.Empty;
            msg = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            //sender email address
            msg.From = new MailAddress("quegolazo.soporte@gmail.com", "Que Golazo!", Encoding.UTF8);
            //Receiver email address
            msg.To.Add(destinatario);
            msg.Subject = asunto;
            msg.Body = cuerpo;
            msg.IsBodyHtml = true;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("quegolazo.soporte@gmail.com", "quegolazo123");
            smtp.Send(msg);
        }

       
        


    }
}
