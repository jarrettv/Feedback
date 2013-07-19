using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JVance.Feedback.Web.Models
{
    public class HomeIndexModel
    {
        public Presentation CurrentPresentation { get; set; }
        public Presentation[] RecentPresentations { get; set; }
        public Presentation[] UpcomingPresentations { get; set; }
    }
}