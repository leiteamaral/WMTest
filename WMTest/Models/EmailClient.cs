using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace WMTest.Models
{
    public class EmailClient
    {
        private static SmtpClient GetSmtpClient(User user)
        {
            try
            {
                var db = new WMTestDbContext();
                var smtpServer = user.Config.Server;
                var smtpPort = user.Config.Port;
                var smtpUsername = user.Config.Username;
                var smtpPassword = user.Config.Password;
                var smtpSSL = user.Config.SSL;
                var basicCredential = new NetworkCredential(smtpUsername, smtpPassword);
                var smtpClient = new SmtpClient()
                {
                    Credentials = basicCredential,
                    Host = smtpServer,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Port = smtpPort,
                    EnableSsl = smtpSSL
                };
                return smtpClient;
            }
            catch (Exception e)
            {
                throw new Exception("Error in SmtpClient Configuration - " + e.Message);
            }
        }

        public static string SendEmailFromWebMaster(string recipient, string body, string subject)
        {
            var db = new WMTestDbContext();
            var webMaster = db.Users.FirstOrDefault(x => x.UserName.Equals("chitestwebmaster"));
            var email = new Email()
            {
                Sender = webMaster, 
                Recipient = recipient, 
                Subject = subject, 
                Body = body
            };
            return SendEmail(email);
        }

        public static string SendEmail(Email email)
        {
            try
            {
                var db = new WMTestDbContext();
                var message = new MailMessage();
                message.To.Add(new MailAddress(email.Recipient));
                message.Subject = email.Subject;
                message.From = new MailAddress(email.Sender.Email, email.Sender.Name);
                message.Body = HttpUtility.HtmlDecode(email.Body);
                message.IsBodyHtml = true;
                
                GetSmtpClient(email.Sender).Send(message);
                db.Emails.Add(email);
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}