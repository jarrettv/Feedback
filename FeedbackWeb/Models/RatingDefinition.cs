using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedbackWeb.Models
{
    public class RatingDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LowName { get; set; }
        public string HighName { get; set; }
    }
}