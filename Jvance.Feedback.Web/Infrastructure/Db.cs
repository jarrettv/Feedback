using System.Data.Entity;
using JVance.Feedback.Web.Models;

namespace JVance.Feedback.Web.Infrastructure

{
    public class Db : DbContext
    {
        public Db() : base("Feedback") {}
        
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<JVance.Feedback.Web.Models.Feedback> Feedbacks { get; set; }
        public DbSet<RatingDefinition> RatingDefinitions { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            // many-to-many
            builder.Entity<Template>()
                .HasMany(x => x.Ratings)
                .WithMany()
                .Map(x => x.MapLeftKey("TemplateId")
                    .MapRightKey("RatingDefId"));

            // composite key
            builder.Entity<Rating>()
                .HasKey(x => new { x.FeedbackId, x.RatingDefinitionId });
        }
    }
}
