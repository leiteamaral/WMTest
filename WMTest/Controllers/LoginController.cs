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
                var err = EmailClient.SendEmailFromWebMaster(uFound.Email, uFound.Name + ", your password is: " + uFound.Password, "Password Recovery");
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
        // GET: /Logout/
        public ActionResult Logout()
        {
            Session["LOGGED_USER"] = null;
            return RedirectToAction("Index", "Home");
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
            var uFound = db.Users.Include(x=>x.Config).FirstOrDefault(x=>x.UserName.Equals(user.UserName) && x.Password.Equals(user.Password));
            if (uFound!=null)
            {
                Session.Add("LOGGED_USER", uFound);
                return RedirectToAction("Index", "Email");
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View();
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
                user.Config = WMDbInitializer.GetDefaultConfig(db);
                db.Users.Add(user);
                db.SaveChanges();
                Session["LOGGED_USER"] = null;
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}