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
    public class ReportController : LoggedUserController
    {
        private WMTestDbContext db = new WMTestDbContext();
        
        //
        // GET: /Report/List
        public ActionResult List()
        {
            return PartialView(GetEmails(db, LoggedUser.ID));
        }

        //
        // POST: /Report/List/
        [HttpPost]
        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetEmails(db, LoggedUser.ID).ToDataSourceResult(request));
        }

        private static IEnumerable<Email> GetEmails(WMTestDbContext db, int idUser)
        {
            return db.Emails.Include(x => x.Sender).Where(x => x.Sender.ID == idUser).ToList();
        }


        public ActionResult Chart()
        {
            var chartData = GetChartData(db, LoggedUser.ID);
            return PartialView(chartData);
        }

        [HttpPost]
        public ActionResult GetChartData()
        {
            var chartData = GetChartData(db, LoggedUser.ID);
            return Json(chartData);
        }

        private static IEnumerable<Chart> GetChartData(WMTestDbContext db, int idUser)
        {
            var emails = db.Emails.Include(x => x.Sender).Where(x => x.Sender.ID == idUser).ToList();
            
            var lstChart = new List<Chart>()
                {
                    new Chart("Monday", emails.Count(e => e.SentDate.DayOfWeek == DayOfWeek.Monday)){Color = "Green"},
                    new Chart("Tuesday", emails.Count(e => e.SentDate.DayOfWeek == DayOfWeek.Tuesday)){Color = "Yellow"},
                    new Chart("Wednesday", emails.Count(e => e.SentDate.DayOfWeek == DayOfWeek.Wednesday)){Color = "Red"},
                    new Chart("Thursday", emails.Count(e => e.SentDate.DayOfWeek == DayOfWeek.Thursday)){Color = "Blue"},
                    new Chart("Friday", emails.Count(e => e.SentDate.DayOfWeek == DayOfWeek.Friday)){Color = "Black"},
                    new Chart("Saturday", emails.Count(e => e.SentDate.DayOfWeek == DayOfWeek.Saturday)){Color = "Orange"},
                    new Chart("Sunday", emails.Count(e => e.SentDate.DayOfWeek == DayOfWeek.Sunday)){Color = "Gray"}
                };
            return lstChart;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}