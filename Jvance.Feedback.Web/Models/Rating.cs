using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JVance.Feedback.Web.Models
{
    public class Rating
    {
        public int FeedbackId { get; set; }
        // NOTE: no navigation property back to feedback

        public int RatingDefinitionId { get; set; }
        public virtual RatingDefinition Definition { get; set; }

        public int Value { get; set; }

        public string Comment { get; set; }
    }
}