using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryResources.Mvc.Components
{
    public class PatientMenu : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var menuItems = new List<MenuItem> { new MenuItem()
                {
                    DisplayValue = "User management",
                    ActionValue = "UserManagement"

                },
                new MenuItem()
                {
                    DisplayValue = "Role management",
                    ActionValue = "Index"
                }};

            return View(menuItems);
        }
    }
}
