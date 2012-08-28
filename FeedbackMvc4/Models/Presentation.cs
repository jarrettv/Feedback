using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FeedbackMvc4.Models
{
    public class Presentation
    {
        public int Id { get; set; } // by convention automatically becomes identity

        [Required, StringLength(200)]
        public string Title { get; set; }

        [Required, StringLength(1000)]
        public string Description { get; set; }

        [Required, StringLength(100)]
        public string Presentor { get; set; }

        [Required, StringLength(20)] 
        public string SecretKey { get; set; }

        public DateTime Date { get; set; }

        public int TemplateId { get; set; } // by convention, automatically becomes foreign key
        public virtual Template Template { get; set; }

        public virtual IList<Feedback> Feedbacks { get; set; } // by convention 1-to-many, lazy loaded
    }
}