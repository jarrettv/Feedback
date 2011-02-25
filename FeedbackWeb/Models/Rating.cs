using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedbackWeb.Models
{
    public class Rating
    {
        public int Id { get; set; }

        public int RatingDefinitionId { get; set; }
        public virtual RatingDefinition Definition { get; set; }

        public int Value { get; set; }
        public string Comment { get; set; }
    }
}