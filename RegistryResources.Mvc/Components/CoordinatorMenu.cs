using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryResources.Mvc.Components
{
    public class CoordinatorMenu : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var menuItems = new List<MenuItem> { new MenuItem()
                {
                    DisplayValue = "Reports",
                    ActionValue = "#"
                },
                new MenuItem()
                {
                    DisplayValue = "Patient Directory",
                    ActionValue = "#"
                },
                new MenuItem()
                {
                    DisplayValue = "Researcher Directory",
                    ActionValue = "#"
                }
            };

            return View(menuItems);
        }
    }
}
