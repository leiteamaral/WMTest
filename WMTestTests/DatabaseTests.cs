using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using WMTest.Models;

// Database.SetInitialize


namespace WMTestTests
{
    [TestClass]
    public class DatabaseTests
    {
        private static readonly WMTestDbContext Db = new WMTestDbContext();
        private static readonly WMDbInitializer DbInit = new WMDbInitializer();

        public static void TestDbInitialze()
        {
            Database.SetInitializer<WMTestDbContext>(DbInit);
        }

        [TestMethod]
        public void GetListEmails()
        {
            TestDbInitialze();
            var users = Db.Emails
                .Include(s => s.Sender)
                .ToList();
        }

        [TestMethod]
        public void GetConfiguration()
        {
            TestDbInitialze();
            var configs = Db.Configurations.ToList();
        }

        [TestMethod]
        public void GetListUsers()
        {
            TestDbInitialze();
            var users = Db.Users
                .Include(c => c.Config)
                .ToList(); 
        }


    }
}
