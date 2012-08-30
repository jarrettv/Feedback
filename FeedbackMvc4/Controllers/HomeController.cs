using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeedbackMvc4.Models;

namespace FeedbackMvc4.Controllers
{
    public class HomeController : Controller
    {
        private readonly FeedbackDb db; // also normal to put inside using statement

        public HomeController(FeedbackDb db)
        {
            this.db = db;
        }

        public ActionResult Index()
        {
            var model = this.db.Presentations
                .AsNoTracking()
                .OrderByDescending(x => x.Date)
                .Take(10)
                .ToArray(); // force execution
            return View(model);
        }

        public ActionResult Feedback(int id)
        {
            var model = new Feedback() { PresentationId = id };

            model.Presentation = this.db.Presentations.Find(id);
            if (model.Presentation == null) return HttpNotFound();

            // force data for display in case not existing
            model.Ratings = model.Presentation.Template.Ratings
                .Select(x => new Rating()
                {
                    RatingDefinitionId = x.Id
                }).ToArray();
            return View(model);
        }

        [HttpPost]
        public ActionResult Feedback(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    feedback.CreatedDate = DateTime.Now;
                    this.db.Feedbacks.Add(feedback);
                    this.db.SaveChanges(); // persist
                    return RedirectToAction("Thanks", new { id = feedback.Id });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex);
                }
            }

            feedback.Presentation = this.db.Presentations.Find(feedback.PresentationId);      
            return View(feedback);
        }

        public ActionResult Thanks(int id)
        {
            var model = this.db.Feedbacks.Find(id);
            if (model.CreatedDate.AddMinutes(1) < DateTimeOffset.Now) 
                return RedirectPermanent(Url.Action("Index")); // SEO
            return View(model);
        }

        public ActionResult Results(string secretKey)
        {
            var model = this.db.Presentations
                .Where(x => x.SecretKey == secretKey)
                .SingleOrDefault();

            if (model == null) return new HttpStatusCodeResult(400, "Bad Request");
            return View(model);
        }

        [ChildActionOnly]
        [OutputCache(Duration=30)]
        public ActionResult FeedbackCount()
        {
            return PartialView("_FeedbackCount", this.db.Feedbacks.Count());
        }
    }
}
