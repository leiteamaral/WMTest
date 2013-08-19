using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMTest.Models;

namespace WMTestTests
{
    [TestClass]
    public class EmailClient
    {
        [TestMethod]
        public void SendEmail()
        {
            DatabaseTests.TestDbInitialze();

            var uTest = new User()
            {
                Email = "leiteamaral@yahoo.com.br",
                Name = "My Test Name",
                Password = "xpto123",
                UserName = "amaral"
            };
            var err = WMTest.Models.EmailClient.SendEmailFromWebMaster(uTest.Email, "Your password is: " + uTest.Password, "Password Recovery");
            Assert.IsNull(err);
        }
    }
}
