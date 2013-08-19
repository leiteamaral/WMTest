using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WMTest.Models;

namespace WMTest.Controllers
{
    public abstract class LoggedUserController : Controller
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (LoggedUser == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Login",
                    action = "Index"
                }));
            }

           
        }

        protected User LoggedUser 
        {
            get
            {
                return Session["LOGGED_USER"] as User;
            }
            set
            {
                Session["LOGGED_USER"] = value;
            }
        }
        
    }
}