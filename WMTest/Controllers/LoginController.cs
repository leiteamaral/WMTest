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
        public ActionResult Forgot(User user)
        {
            if (String.IsNullOrEmpty(user.UserName))
            {
                ModelState.AddModelError("", "Please fill the Username field");
                return View(user);
            }
            var uFound = db.Users.ToList().FirstOrDefault(x => x.UserName.Equals(user.UserName));
            if (uFound!=null)
            {
                var err = EmailClient.SendEmailFromWebMaster(uFound, uFound.Name + ", your password is: " + uFound.Password, "Password Recovery");
                if (err == null)
                {
                    user.Email = uFound.Email;
                }
                ModelState.AddModelError("", err);
            }
            else
            {
                ModelState.AddModelError("", String.Format("Username '{0}' not found in our database", user.UserName));   
            }
            return View(user);
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
                var u = db.Users.FirstOrDefault(x => x.UserName.Equals(user.UserName));
                if (u != null)
                {
                    ModelState.AddModelError("", "This Username is already in use, please type other");
                    return View(user);
                }
                u = db.Users.FirstOrDefault(x => x.Email.Equals(user.Email));
                if (u != null)
                {
                    ModelState.AddModelError("", "This Email is already in use, please type other");
                    return View(user);
                }
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
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