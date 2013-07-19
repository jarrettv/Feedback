using JVance.Feedback.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapMvcWebsite.Controllers
{
    public class PresentationController : Controller
    {
        private Db db = new Db();

        public ViewResult Index(bool old = false)
        {
            var oldDate = DateTime.Today.AddMonths(-2);

            var q = this.db.Presentations.AsNoTracking().AsQueryable();
            
            // simple filter
            if (old) q = q.Where(x => x.Date < oldDate);
            else q = q.Where(x => x.Date >= oldDate);
            
            var m = q.OrderByDescending(x => x.Date).ToArray();

            return this.View("Index", m);
        }

        public ViewResult Historic()
        {
            return this.Index(old: true);
        }

        protected override void Dispose(bool disposing)
        {
            this.db.Dispose();
            base.Dispose(disposing);
        }
    }
}
