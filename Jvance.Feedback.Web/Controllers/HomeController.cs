using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JVance.Feedback.Web.Models;
using JVance.Feedback.Web.Infrastructure;

namespace JVance.Feedback.Web.Controllers
{
    public class HomeController : Controller
    {
        private Db db = new Db();

        public ActionResult Index()
        {
            var m = new HomeIndexModel();
            var fewHoursAgo = DateTime.Now.AddHours(-2);
            m.CurrentPresentation = db.Presentations
                .Where(x => x.Date >= fewHoursAgo)
                .OrderBy(x => x.Date)
                .FirstOrDefault();

            m.UpcomingPresentations = db.Presentations
                .Where(x => x.Date > DateTime.Now)
                .OrderBy(x => x.Date)
                .Take(5)
                .ToArray();

            m.RecentPresentations = db.Presentations
                .Where(x => x.Date < DateTime.Now)
                .OrderByDescending(x => x.Date)
                .Take(5)
                .ToArray();
            return this.View(m);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult CodeStock()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            this.db.Dispose();
            base.Dispose(disposing);
        }
    }
}