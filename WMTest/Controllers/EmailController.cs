using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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
                return RedirectToAction("Index", new RouteValueDictionary() { { "aTab", "0" }, { "Result", err } });
            }
            else
            {
                db.Emails.Add(sendEmail);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
        //
        // GET: /Email/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Email email = db.Emails.Find(id);
            if (email == null)
            {
                return HttpNotFound();
            }
            return View(email);
        }

        //
        // POST: /Email/Edit/5

        [HttpPost]
        public ActionResult Edit(Email email)
        {
            if (ModelState.IsValid)
            {
                db.Entry(email).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(email);
        }

        //
        // GET: /Email/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Email email = db.Emails.Find(id);
            if (email == null)
            {
                return HttpNotFound();
            }
            return View(email);
        }

        //
        // POST: /Email/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Email email = db.Emails.Find(id);
            db.Emails.Remove(email);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}