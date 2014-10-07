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
        public void mandarMailActivacion(string destinatario, string asunto, string urlActivación)
        {
            MailMessage msg;
            msg = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            //sender email address
            msg.From = new MailAddress("quegolazo.soporte@gmail.com", "Que Golazo!", Encoding.UTF8);
            //Receiver email address
            msg.To.Add(destinatario);
            msg.Subject = asunto;
            string body= @"
                    <body bgcolor=""#f6f6f6"" style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; -webkit-font-smoothing: antialiased; -webkit-text-size-adjust: none; width: 100% !important; height: 100%; margin: 0; padding: 0;"">&#13;
                    &#13;
                    &#13;
                    <table style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; width: 100%; margin: 0; padding: 20px;""><tr style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; color: #fff; display: block !important; max-width: 600px !important; clear: both !important; background-color: #60A64F; margin: 0 auto; padding: 15px 20px;"" bgcolor=""#60A64F""><td style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 18px; line-height: 1.6; margin: 0; padding: 0;"">Bienvenido a QueGolazo!</td></tr><tr style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;""><td bgcolor=""#FFFFFF"" style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto; padding: 10px 20px; border: 1px solid #f0f0f0;"">&#13;
                    &#13;
			        &#13;
			        <div style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; max-width: 600px; display: block; margin: 0 auto; padding: 0;"">&#13;
			        <table style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; width: 100%; margin: 0; padding: 0;""><tr style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;""><td style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;"">&#13;
						        <p style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 1.6; font-weight: normal; margin: 0 0 5px; padding: 0;"">Has registrado una nueva cuenta en <a href=""http://www.quegolazo.com"" style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; color: #60A64F; margin: 0; padding: 0;"">QueGolazo.com</a></p>&#13;
						        <p style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 1.6; font-weight: normal; margin: 0 0 5px; padding: 0;"">Para activar tu cuenta debes hacer clic en el enlace a continuación</p>&#13;
						        <table style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; width: 100%; margin: 0; padding: 0;""><tr style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;""><td style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 10px 0;"">&#13;
									        <p style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 1.6; font-weight: normal; margin: 0 0 5px; padding: 0;""><a href='";
                    body+=urlActivación;
                    body+=@"' style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 2; color: #FFF; text-decoration: none; font-weight: bold; text-align: center; cursor: pointer; display: inline-block; border-radius: 5px; background-color: #82b47b; margin: 0 10px 0 0; padding: 0; border-color: #82b47b; border-style: solid; border-width: 5px 10px;"">Activar Cuenta</a></p>&#13;
								        </td>&#13;
							        </tr></table><p style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 1.6; font-weight: normal; margin: 0 0 5px; padding: 0;"">Si tenes algún problema con botón, copiá el siguiente link: </p>&#13;
						        <p style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 11px; line-height: 1.6; font-weight: normal; margin: 0 0 5px; padding: 0;"">";
                    body+=urlActivación;
                    body+=@" </p>&#13;
					        </td>&#13;
				        </tr></table></div>&#13;
			        &#13;
			        &#13;
		            </td>&#13;
		            <td style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;""></td>&#13;
	                </tr></table><table style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; width: 100%; clear: both !important; margin: 0; padding: 0;""><tr style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;""><td style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;""></td>&#13;
		            <td style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto; padding: 0;"">&#13;
			        &#13;
			        &#13;
			        <div style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; max-width: 600px; display: block; margin: 0 auto; padding: 0;"">&#13;
				        <table style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; width: 100%; margin: 0; padding: 0;""><tr style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;""><td align=""center"" style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;"">&#13;
							        <p style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 12px; line-height: 1.6; color: #666; font-weight: normal; margin: 0 0 5px; padding: 0;"">No queres recibir más mails de QueGolazo? <a href="""" style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; color: #999; margin: 0; padding: 0;""><unsubscribe style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;"">Cancelar Subscripción</unsubscribe></a>.&#13;
							        </p>&#13;
						        </td>&#13;
					        </tr></table></div>&#13;
			        &#13;
			        &#13;
		            </td>&#13;
		            <td style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;""></td>&#13;
	                </tr></table></body>";
            msg.Body =body;
            msg.IsBodyHtml = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
          
            smtp.Credentials = new System.Net.NetworkCredential("quegolazo.soporte@gmail.com", "quegolazo123");
            smtp.EnableSsl = true;
            smtp.Send(msg);
        }

        /// <summary>
        /// metodo para mandar mails
        /// autor: Flor
        /// </summary>
        /// <param name="destinatario"></param>
        /// <param name="asunto"></param>
        /// <param name="cuerpo"></param>
        public void mandarMailRecuperacion(string destinatario, string asunto, string urlRecuperacion)
        {
            MailMessage msg;
            msg = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            //sender email address
            msg.From = new MailAddress("quegolazo.soporte@gmail.com", "Que Golazo!", Encoding.UTF8);
            //Receiver email address
            msg.To.Add(destinatario);
            msg.Subject = asunto;
            string body = @"
                    <body bgcolor=""#f6f6f6"" style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; -webkit-font-smoothing: antialiased; -webkit-text-size-adjust: none; width: 100% !important; height: 100%; margin: 0; padding: 0;"">&#13;
                    &#13;
                    &#13;
                    <table style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; width: 100%; margin: 0; padding: 20px;""><tr style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; color: #fff; display: block !important; max-width: 600px !important; clear: both !important; background-color: #60A64F; margin: 0 auto; padding: 15px 20px;"" bgcolor=""#60A64F""><td style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 18px; line-height: 1.6; margin: 0; padding: 0;"">Recuperación de clave de acceso para QueGolazo!</td></tr><tr style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;""><td bgcolor=""#FFFFFF"" style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto; padding: 10px 20px; border: 1px solid #f0f0f0;"">&#13;
                    &#13;
			        &#13;
			        <div style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; max-width: 600px; display: block; margin: 0 auto; padding: 0;"">&#13;
			        <table style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; width: 100%; margin: 0; padding: 0;""><tr style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;""><td style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;"">&#13;
						        <p style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 1.6; font-weight: normal; margin: 0 0 5px; padding: 0;"">Este mail fue enviado para iniciar el proceso de recuperación de la cuenta asociada con esta dirección de mail.</p>&#13;
						        <p style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 1.6; font-weight: normal; margin: 0 0 5px; padding: 0;"">Para reestablecer tu clave debes hacer clic en el enlace a continuación</p>&#13;
						        <table style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; width: 100%; margin: 0; padding: 0;""><tr style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;""><td style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 10px 0;"">&#13;
									        <p style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 1.6; font-weight: normal; margin: 0 0 5px; padding: 0;""><a href='";
            body += urlRecuperacion;
            body += @"' style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 2; color: #FFF; text-decoration: none; font-weight: bold; text-align: center; cursor: pointer; display: inline-block; border-radius: 5px; background-color: #82b47b; margin: 0 10px 0 0; padding: 0; border-color: #82b47b; border-style: solid; border-width: 5px 10px;"">Recuperar Contraseña</a></p>&#13;
								        </td>&#13;
							        </tr></table><p style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 1.6; font-weight: normal; margin: 0 0 5px; padding: 0;"">Si tenes algún problema con botón, copiá el siguiente link: </p>&#13;
						        <p style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 11px; line-height: 1.6; font-weight: normal; margin: 0 0 5px; padding: 0;"">";
            body += urlRecuperacion;
            body += @" </p>&#13;
					        </td>&#13;
				        </tr></table></div>&#13;
			        &#13;
			        &#13;
		            </td>&#13;
		            <td style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;""></td>&#13;
	                </tr></table><table style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; width: 100%; clear: both !important; margin: 0; padding: 0;""><tr style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;""><td style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;""></td>&#13;
		            <td style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto; padding: 0;"">&#13;
			        &#13;
			        &#13;
			        <div style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; max-width: 600px; display: block; margin: 0 auto; padding: 0;"">&#13;
				        <table style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; width: 100%; margin: 0; padding: 0;""><tr style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;""><td align=""center"" style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;"">&#13;
							        <p style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 12px; line-height: 1.6; color: #666; font-weight: normal; margin: 0 0 5px; padding: 0;"">No queres recibir más mails de QueGolazo? <a href="""" style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; color: #999; margin: 0; padding: 0;""><unsubscribe style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;"">Cancelar Subscripción</unsubscribe></a>.&#13;
							        </p>&#13;
						        </td>&#13;
					        </tr></table></div>&#13;
			        &#13;
			        &#13;
		            </td>&#13;
		            <td style=""font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size: 100%; line-height: 1.6; margin: 0; padding: 0;""></td>&#13;
	                </tr></table></body>";
            msg.Body = body;
            msg.IsBodyHtml = true;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("quegolazo.soporte@gmail.com", "quegolazo123");
            smtp.Send(msg);
        }


    }
}
