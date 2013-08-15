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
            var configs = CreateConfigs(context);
        }


        private List<User> CreateUsers(WMTestDbContext context)
        {
            var users = new List<User> 
            {
                new User {UserName = "joao", Name = "João Paulo", Password = "12345", Email = "leiteamaral@yahoo.com.br"}, 
                new User {UserName = "maria", Name = "Maria Aparecida", Password = "12345", Email = "leiteamaral@yahoo.com.br"},
                new User {UserName = "fred", Name = "Frederico Gama", Password = "12345", Email = "leiteamaral@yahoo.com.br"},
                new User {UserName = "sa", Name = "Samantha Leite", Password = "12345", Email = "leiteamaral@yahoo.com.br"},
                new User {UserName = "proberto", Name = "Paulo Roberto", Password = "12345", Email = "leiteamaral@yahoo.com.br"},
                new User {UserName = "pedrao", Name = "Pedro Augusto", Password = "12345", Email = "leiteamaral@yahoo.com.br"},
                new User {UserName = "tsilva", Name = "Tariana Silva", Password = "12345", Email = "leiteamaral@yahoo.com.br"},
                new User {UserName = "poliveira", Name = "Paola Oliveira", Password = "12345", Email = "leiteamaral@yahoo.com.br"},
                new User {UserName = "ssatto", Name = "Sabrina Satto", Password = "12345", Email = "poliveira@testemailserver.com.br"},
                new User {UserName = "lpiovani", Name = "Luana Piovani", Password = "12345", Email = "leiteamaral@yahoo.com.br"}
            };
            users.ForEach(d => context.Users.Add(d));

            return users;
        }
        
        private List<Configuration> CreateConfigs(WMTestDbContext context)
        {
            var configs = new List<Configuration>
            {
               new Configuration(){Name = "Sever", Value = "mail.yahoo.com.br"},
               new Configuration(){Name = "Port", Value = "51"},
               new Configuration(){Name = "ServerTimeout", Value = "10000"}
            };
            configs.ForEach(d => context.Configurations.Add(d));

            return configs;
        }

        private List<Email> CreateEmails(WMTestDbContext context, IEnumerable<User> users)
        {
            var emails = new List<Email>();
            var testDate = DateTime.Now.AddDays(-1 * NumberOfEmails);
            var iS = 0;
            var iR = 0;

            //Create a list of emails to populate a test database
            for (var i = 0; i < NumberOfEmails; i++)
            {                
                var email = new Email()
                    {
                        Sender = users.ElementAt(iS),
                        SentDate = (testDate = testDate.AddDays(i)),
                        Subject = "Testing",
                        Recipient = users.ElementAt(iR),
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
                    iR += iR < users.Count() - 1 ? 1 : -1 * iR; 
                }
            }
            return emails;
        }
        

    }
    
    
}