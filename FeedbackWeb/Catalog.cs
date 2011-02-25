using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FeedbackWeb.Models;
using System.Data.Entity.Database;

namespace FeedbackWeb
{
    public class Catalog : DbContext
    {
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<RatingDefinition> RatingDefinitions { get; set; }
    }

    public class CatalogSeed : DropCreateDatabaseIfModelChanges<Catalog>
    {
        protected override void Seed(Catalog context)
        {
            var template = new Template()
            {
                Name = "Tech Template",
                Ratings = new List<RatingDefinition>()
                {
                    new RatingDefinition() { Name = "Initial Impressions", LowName = "Tense", HighName = "Poised" },
                    new RatingDefinition() { Name = "The Big Picture", LowName = "Not mentioned", HighName = "Clearly stated" },
                    new RatingDefinition() { Name = "Link with audience", LowName = "Nope", HighName = "Yep" },
                    new RatingDefinition() { Name = "Mannerisms", LowName = "Distracting", HighName = "None noticed" },
                    new RatingDefinition() { Name = "Jargon", LowName = "Excessive", HighName = "None noticed" },
                    new RatingDefinition() { Name = "Visual aids", LowName = "Unclear", HighName = "Effective" },
                    new RatingDefinition() { Name = "Conclusions", LowName = "Muddled", HighName = "Stated clearly" },
                    new RatingDefinition() { Name = "Take-home message", LowName = "None stated", HighName = "Memorable" },
                    new RatingDefinition() { Name = "Final impression", LowName = "Needs work", HighName = "Great job" },
                }
            };

            context.Templates.Add(template);

            context.Presentations.Add(new Presentation()
            {
                Title = "Jump Into MVC3",
                Description = "ASP.NET MVC 3 is a framework for building scalable, standards-based web applications using well-established design patterns and the power of ASP.NET and the .NET Framework. The latest version includes great new features: razor view engine, expanded validation, and built in support for dependency injection.",
                Presentor = "Jarrett Vance",
                Template = template,
                SecretKey = "mario1",
                Date = new DateTimeOffset(2011, 2, 10, 17, 30, 00, TimeZoneInfo.Local.BaseUtcOffset).DateTime
            });

            context.SaveChanges();
        }
    }
}