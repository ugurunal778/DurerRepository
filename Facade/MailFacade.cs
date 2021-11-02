using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Facade
{
    public class MailFacade : FacadeBase, IMailFacade
    {
        public void SendMail(string body, string fullName, string title)
        {
            string to = "info@durer.com.tr"; //To address    
            string from = "haznedargroupmailer@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = body;
            message.Subject = title + " - " + fullName;
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("haznedargroupmailer@gmail.com", "Haznedargroup1.");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;

            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
