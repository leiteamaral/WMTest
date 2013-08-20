using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using WMTest.Models;

namespace WMTest.Controllers
{
    public class EmailController : LoggedUserController
    {
        private WMTestDbContext db = new WMTestDbContext();

        //
        // GET: /Email?aTab=5&result=Success
        public ActionResult Index(int aTab = 0, string result = "")
        {
            ViewBag.aTab = aTab;
            ModelState.AddModelError("", result);
            return View(new Email(){Sender = LoggedUser});
        }

        //
        // GET: /Email/Send
        public ActionResult Send()
        {
            return PartialView(new Email() { Sender = LoggedUser });
        }

        //
        // POST: /Email/Send
        [HttpPost]
        public ActionResult Send(Email email)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(email);
            }

            var sendEmail = new Email()
            {
                Body = email.Body,
                Recipient = email.Recipient,
                Sender = LoggedUser,
                SentDate = DateTime.Now,
                Subject = email.Subject
            };
            
            var err = EmailClient.SendEmail(sendEmail);

            if (err != null)
            {
                ModelState.AddModelError("", err);
                return PartialView(sendEmail);
            }
            else
            {
                ViewBag.Result = "Success sending email!!!";
            }

            db.Users.Attach(sendEmail.Sender);
            db.Emails.Add(sendEmail);
            db.SaveChanges();
            return PartialView(sendEmail);
        }
        

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}