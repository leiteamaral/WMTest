using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMTest.Models;

namespace WMTest.Controllers
{
    public class ConfigController : Controller
    {
        private WMTestDbContext db = new WMTestDbContext();

        //
        // GET: /Config/

        public ActionResult Index()
        {
            return View(db.Configurations.ToList());
        }

        //
        // GET: /Config/Details/5

        public ActionResult Details(int id = 0)
        {
            Configuration configuration = db.Configurations.Find(id);
            if (configuration == null)
            {
                return HttpNotFound();
            }
            return View(configuration);
        }

        //
        // GET: /Config/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Config/Create

        [HttpPost]
        public ActionResult Create(Configuration configuration)
        {
            if (ModelState.IsValid)
            {
                db.Configurations.Add(configuration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(configuration);
        }

        //
        // GET: /Config/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Configuration configuration = db.Configurations.Find(id);
            if (configuration == null)
            {
                return HttpNotFound();
            }
            return View(configuration);
        }

        //
        // POST: /Config/Edit/5

        [HttpPost]
        public ActionResult Edit(Configuration configuration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(configuration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(configuration);
        }

        //
        // GET: /Config/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Configuration configuration = db.Configurations.Find(id);
            if (configuration == null)
            {
                return HttpNotFound();
            }
            return View(configuration);
        }

        //
        // POST: /Config/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Configuration configuration = db.Configurations.Find(id);
            db.Configurations.Remove(configuration);
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