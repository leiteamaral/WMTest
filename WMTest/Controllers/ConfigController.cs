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

        [HttpPost]
        public ActionResult Index(Configuration config)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(config).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Email", new RouteValueDictionary() { { "aTab", "0" } });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Email", new RouteValueDictionary() { { "aTab", "0" }, { "Result", "Error!!!" } });
            }
            
            
        }
       

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}