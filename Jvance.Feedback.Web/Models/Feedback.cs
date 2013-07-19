using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace JVance.Feedback.Web.Models
{
    public class Feedback : IValidatableObject
    {
        public int Id { get; set; }

        [Required, StringLength(100), MinLength(1)]
        public string Name { get; set; }

        [Required, RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
        public string Email { get; set; }
        
        [AllowHtml]
        public string Message { get; set; }
                
        public int PresentationId { get; set; }
        public virtual Presentation Presentation { get; set; }

        public virtual IList<Rating> Ratings { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Ratings.Where(r => r.Value != 0).Count() < 3)
                yield return new ValidationResult("You must set at least three ratings.");
        }
    }
}