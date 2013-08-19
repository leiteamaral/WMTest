using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WMTest.Models
{
    public class WMDbInitializer : DropCreateDatabaseIfModelChanges<WMTestDbContext> {

        private const int NumberOfEmails = 20;
        
        protected override void Seed(WMTestDbContext context)
        {
            var users = CreateUsers(context);
            var emails = CreateEmails(context, users);
            context.SaveChanges();
        }


        private List<User> CreateUsers(WMTestDbContext context)
        {
            var users = new List<User> 
            {
                new User {UserName = "chitestwebmaster", Name = "WebMaster", Password = "12345", Email = "chitestwebmaster@gmail.com", Config = GetDefaultConfig(context)},
                new User {UserName = "joao", Name = "João Paulo", Password = "12345", Email = "leiteamaral@yahoo.com.br", Config = GetDefaultConfig(context)}, 
                new User {UserName = "maria", Name = "Maria Aparecida", Password = "12345", Email = "leiteamaral@yahoo.com.br", Config = GetDefaultConfig(context)},
                new User {UserName = "fred", Name = "Frederico Gama", Password = "12345", Email = "leiteamaral@yahoo.com.br", Config = GetDefaultConfig(context)},
                new User {UserName = "sa", Name = "Samantha Leite", Password = "12345", Email = "leiteamaral@yahoo.com.br", Config = GetDefaultConfig(context)},
                new User {UserName = "proberto", Name = "Paulo Roberto", Password = "12345", Email = "leiteamaral@yahoo.com.br", Config = GetDefaultConfig(context)},
                new User {UserName = "pedrao", Name = "Pedro Augusto", Password = "12345", Email = "leiteamaral@yahoo.com.br", Config = GetDefaultConfig(context)},
                new User {UserName = "tsilva", Name = "Tariana Silva", Password = "12345", Email = "leiteamaral@yahoo.com.br", Config = GetDefaultConfig(context)},
                new User {UserName = "poliveira", Name = "Paola Oliveira", Password = "12345", Email = "leiteamaral@yahoo.com.br", Config = GetDefaultConfig(context)},
                new User {UserName = "ssatto", Name = "Sabrina Satto", Password = "12345", Email = "poliveira@testemailserver.com.br", Config = GetDefaultConfig(context)},
                new User {UserName = "lpiovani", Name = "Luana Piovani", Password = "12345", Email = "leiteamaral@yahoo.com.br", Config = GetDefaultConfig(context)}
            };
            users.ForEach(d => context.Users.Add(d));

            return users;
        }

        public static Configuration GetDefaultConfig(WMTestDbContext context)
        {
            var config = new Configuration()
            {
                Server = "smtp.gmail.com",
                Port = 587,
                SSL = true,
                Username = "chitestwebmaster@gmail.com",
                Password = "unifei12345"
            };
            context.Configurations.Add(config);
            return config;
        }

        private List<Email> CreateEmails(WMTestDbContext context, IEnumerable<User> users)
        {
            var emails = new List<Email>();
            var testDate = DateTime.Now.AddDays(-1 * NumberOfEmails);
            var iS = 0;
            
            //Create a list of emails to populate a test database
            for (var i = 0; i < NumberOfEmails; i++)
            {                
                var email = new Email()
                    {
                        Sender = users.ElementAt(iS),
                        SentDate = (testDate = testDate.AddDays(i)),
                        Subject = "Testing",
                        Recipient = String.Format("test{0}@mailserver.com", i),
                        Body = "This is a boby test only"
                    };
                context.Emails.Add(email);
                emails.Add(email);
                               

                if (iS < users.Count() - 1)
                {
                    iS++;
                }
                else
                {
                    iS = 0;
                }
            }
            return emails;
        }
        

    }
    
    
}