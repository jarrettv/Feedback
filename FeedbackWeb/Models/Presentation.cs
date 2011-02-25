using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FeedbackWeb.Models
{
    public class Presentation
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Presentor { get; set; }

        [Required] 
        public string SecretKey { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public virtual Template Template { get; set; }

        public virtual IList<Feedback> Feedbacks { get; set; }
    }
}