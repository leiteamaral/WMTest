using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using WebGrease.Css.Extensions;
using System.Data.Entity;

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
                    UseDefaultCredentials = !smtpSSL,
                    Host = smtpServer,
                    Port = smtpPort,
                    EnableSsl = smtpSSL,
                    Credentials = basicCredential
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
            var webMaster = db.Users.Include(x=>x.Config).FirstOrDefault(x => x.UserName.Equals("chitestwebmaster"));

            var email = new Email()
            {
                Sender = webMaster, 
                Recipient = recipient, 
                Subject = subject, 
                Body = body
            };
            return SendEmail(email, null);
        }

        public static string SendEmail(Email email, IEnumerable<string> attachaments)
        {
            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(email.Recipient));
                message.Subject = email.Subject;
                message.From = new MailAddress(email.Sender.Email, email.Sender.Name);
                
                message.Body = HttpUtility.HtmlDecode(email.Body);
                message.IsBodyHtml = true;
                message.HeadersEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.UTF8;

                if (attachaments != null && attachaments.Any())
                {
                    attachaments.ForEach(x => message.Attachments.Add(new Attachment(x)));
                }
                GetSmtpClient(email.Sender).Send(message);
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}