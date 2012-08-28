using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedbackMvc4.Models
{
    public class Template
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual IList<RatingDefinition> Ratings { get; set; }
    }
}