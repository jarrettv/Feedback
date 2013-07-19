using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aaa.Common;
using Jvance.Feedback.Web.Models;

namespace Cts.Chronos.Web
{
    public class MenuConfig
    {
        public static void RegisterMenus(IDictionary<string, Menu> menus)
        {
            var mainMenu = new Menu();

            mainMenu.Items.Add(new MenuItem("Presentations")
            {
                Children = 
                {
                    new MenuItem("Current") { RouteName = "Presentations" },
                    new MenuItem("Historic (2+ months ago)") { RouteName = "Historic" },
                }
            });

            mainMenu.Items.Add(new MenuItem("CodeStock"));
            mainMenu.Items.Add(new MenuItem("About"));

            mainMenu.Items.Add(new MenuItem("Admin")
            {
                Children =
                {
                    new MenuItem("Results", "Admin", Roles.Admin),
                    new MenuItem("Templates", Roles.Admin),
                    new MenuItem("Ratings", Roles.Admin)
                }
            });
                        
            menus.Add("main", mainMenu);
        }
    }
}