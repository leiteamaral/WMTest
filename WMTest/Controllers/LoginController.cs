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
    public class LoginController : Controller
    {
        private WMTestDbContext db = new WMTestDbContext();


        //
        // GET: /Login/Forgot
        public ActionResult Forgot()
        {
            return View();
        }


        //
        // POST: /Login/Forgot
        [HttpPost]
        public ActionResult Forgot(List<string> p)
        {
            if ("".Equals(p[0]) && "".Equals(p[1]))
            {
                ModelState.AddModelError("", "Please fill the least a field");
                return View();
            }

            var uFound = from u in db.Users.ToList()
                         where u.UserName.Equals(p[0]) || u.Email.Equals(p[1])
                         select u;
            
            if (uFound.Any())
            {
                var email = uFound.First().Email;
                p.Add(email);
                return View(p);
            }
            
            ModelState.AddModelError("", "Username or Email not found in our database");
            return View();
        }

        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Login/
        [HttpPost]
        public ActionResult Index(User user)
        {
            var res = from u in db.Users where u.UserName.Equals(user.UserName) && u.Password.Equals(user.Password) select u;
            if (res.Any())
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        //
        // POST: /Login/Details
        [HttpPost]
        public ActionResult Details(User user)
        {
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /Login/Details/5

        public ActionResult Details(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /Login/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Login/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        //
        // GET: /Login/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Login/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /Login/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Login/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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