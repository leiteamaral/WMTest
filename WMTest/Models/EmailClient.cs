using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace WMTest.Models
{
    public class EmailClient
    {
        private static SmtpClient GetSmtpClient()
        {
            try
            {
                var db = new WMTestDbContext();
                var smtpServer = db.Configurations.First(x => x.Name.Equals("SmtpServer")).Value;
                var smtpPort = db.Configurations.First(x => x.Name.Equals("SmtpPort")).Value;
                var smtpUsername = db.Configurations.First(x => x.Name.Equals("SmtpUsername")).Value;
                var smtpPassword = db.Configurations.First(x => x.Name.Equals("SmtpPassword")).Value;
                var smtpSSL = !db.Configurations.First(x => x.Name.Equals("SmtpSSL")).Value.Equals("0");
                var basicCredential = new NetworkCredential(smtpUsername, smtpPassword);
                var smtpClient = new SmtpClient()
                {
                    Credentials = basicCredential,
                    Host = smtpServer,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Port = int.Parse(smtpPort),
                    EnableSsl = smtpSSL
                };
                return smtpClient;
            }
            catch (Exception e)
            {
                throw new Exception("Error in SmtpClient Configuration - " + e.Message);
            }
        }

        public static string SendEmailFromWebMaster(User recipient, string body, string subject)
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
                message.To.Add(new MailAddress(email.Recipient.Email, email.Recipient.Name));
                message.Subject = email.Subject;
                message.From = new MailAddress(email.Sender.Email, email.Sender.Name);
                message.Body = email.Body;
                GetSmtpClient().Send(message);
                db.Emails.Add(email);
                return null;
            }
            catch (Exception e)
            {
                return "Error sending email - " + e.Message;
            }
        }

    }
}