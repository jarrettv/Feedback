using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeedbackWeb.Models;

namespace FeedbackWeb.Controllers
{
    public class HomeController : Controller
    {
        private Catalog catalog;

        public HomeController()
        {
            this.catalog = new Catalog();
            //this.catalog = c;
        }

        public ActionResult Index()
        {
            var model = this.catalog.Presentations.OrderByDescending(x => x.Date).Take(10).ToList();
            return View(model);
        }

        public ActionResult Feedback(int id)
        {
            var m = new Feedback() { PresentationId = id };
            m.Presentation = this.catalog.Presentations.Find(id);
            if (m.Presentation == null) return HttpNotFound();
            m.Ratings = m.Presentation.Template.Ratings.Select(x => new Rating()
            {
                RatingDefinitionId = x.Id
            }).ToList();
            return View(m);
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Feedback(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    feedback.CreatedDate = DateTime.Now;
                    this.catalog.Feedbacks.Add(feedback);
                    this.catalog.SaveChanges();
                    return RedirectToAction("Thanks", new { id = feedback.Id });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex);
                }
            }

            feedback.Presentation = this.catalog.Presentations.Find(feedback.PresentationId);      
            return View(feedback);
        }

        public ActionResult Thanks(int id)
        {
            var m = this.catalog.Feedbacks.Find(id);
            if (m.CreatedDate.AddMinutes(1) < DateTimeOffset.Now) 
                return RedirectPermanent(Url.Action("Index")); // SEO
            return View(m);
        }

        public ActionResult Results(string secretKey)
        {
            var m = this.catalog.Presentations.Where(x => x.SecretKey == secretKey).SingleOrDefault();
            if (m == null) return new HttpStatusCodeResult(400, "Bad Request");
            return View(m);
        }

        [ChildActionOnly]
        [OutputCache(Duration=30)]
        public ActionResult FeedbackCount()
        {
            return PartialView("_FeedbackCount", this.catalog.Feedbacks.Count());
        }
    }
}
