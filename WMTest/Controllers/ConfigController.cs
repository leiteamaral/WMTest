using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Routing;
using WMTest.Models;

namespace WMTest.Controllers
{
    public class ConfigController : LoggedUserController
    {
        private WMTestDbContext db = new WMTestDbContext();

        //
        // GET: /Config/5
        public ActionResult Index(string result)
        {
            ViewBag.Result = result ?? "";
            return PartialView(LoggedUser.Config);
        }

        //
        // POST: /Config/5
        [HttpPost]
        public ActionResult Index(Configuration config)
        {
            if (ModelState.IsValid)
            {
                db.Entry(config).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Result = "Success, configurations saved!!!";
            }
            return PartialView(LoggedUser.Config);
        }
       

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}