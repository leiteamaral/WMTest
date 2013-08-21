using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
            UploadedFiles = null;
            return PartialView(new Email() { Sender = LoggedUser });
        }

         //
        // POST: /Email/Send
        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> attachments)
        {
            // The Name of the Upload component is "attachments"
            foreach (var file in attachments)
            {
                // Some browsers send file names with full path. We only care about the file name.
                var fileName = Path.GetFileName(file.FileName);
                var destinationPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                file.SaveAs(destinationPath);
                UploadedFiles.Add(destinationPath);
            }
            // Return an empty string to signify success
            return Content("");
        }
        
        private List<string> UploadedFiles
        {
            get
            {
                return Session["Upload_Files"] as List<string> ?? (List<string>)(Session["Upload_Files"] = new List<string>());
            }
            set
            {
                Session["Upload_Files"] = value;
            }
        }

        public ActionResult RemoveUploadedFiles(string[] fileNames)
        {
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
                UploadedFiles.Remove(physicalPath);
            }
            return Content("");
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

            var err = EmailClient.SendEmail(sendEmail, UploadedFiles);

            if (err != null)
            {
                ModelState.AddModelError("", err);
                return PartialView(sendEmail);
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