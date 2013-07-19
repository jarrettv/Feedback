namespace JVance.Feedback.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using JVance.Feedback.Web.Models;
    using System.Collections.Generic;
    using JVance.Feedback.Web.Infrastructure;

    internal sealed class Configuration : DbMigrationsConfiguration<Db>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Db context)
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

            context.Templates.AddOrUpdate(template);

            context.Presentations.AddOrUpdate(new Presentation()
            {
                Id = 1,
                Title = "Jump Into MVC3",
                Description = "ASP.NET MVC 3 is a framework for building scalable, standards-based web applications using well-established design patterns and the power of ASP.NET and the .NET Framework. The latest version includes great new features: razor view engine, expanded validation, and built in support for dependency injection.",
                Presentor = "Jarrett Vance",
                Template = template,
                SecretKey = "mario1",
                Date = new DateTimeOffset(2011, 2, 10, 17, 30, 00, TimeZoneInfo.Local.BaseUtcOffset).DateTime
            });

            context.Presentations.AddOrUpdate(new Presentation()
            {
                Id = 2,
                Title = "EF5 Code First",
                Description = "Code First is a new development pattern for the ADO.NET Entity Framework and provides an alternative to the existing Database First and Model First patterns. Code First is focused around defining your model using C#/VB.NET classes, these classes can then be mapped to an existing database or used to generate a database schema. Additional configuration can be supplied using data annotations or via a fluent API. Code First supports migrations for easily versioning your database.",
                Presentor = "Jarrett Vance",
                Template = template,
                SecretKey = "mario2",
                Date = new DateTimeOffset(2012, 8, 28, 17, 00, 00, TimeZoneInfo.Local.BaseUtcOffset).DateTime
            });

            context.Presentations.AddOrUpdate(new Presentation()
            {
                Id = 3,
                Title = "Bootstrap an MVC Website",
                Description = "Bootstrap by Twitter is taking over the internet. It seems like all new websites are using this powerful front-end framework. In this session, I’ll take a look at why bootstrap is awesome and I’ll teach you how to apply it to an ASP.NET MVC web application.",
                Presentor = "Jarrett Vance",
                Template = template,
                SecretKey = "boot3",
                Date = new DateTimeOffset(2013, 7, 13, 15, 10, 00, TimeZoneInfo.Local.BaseUtcOffset).DateTime
            });
        }
    }
}
