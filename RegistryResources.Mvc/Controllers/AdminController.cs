using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RegistryResources.Mvc.Controllers
{
    //[Authorize(Roles = "Administrator")]
    //[Authorize(Roles = "Coordinator")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task< IActionResult> Index()
        {
            //if (_roleManager.Roles.Where(r => r.Name == "Researcher").Count() == 0)
            //{
            //    var newRole = new IdentityRole()
            //    {
            //        Name = "Researcher"
            //    };

            //    var result = await _roleManager.CreateAsync(newRole);
            //}

            //if (_roleManager.Roles.Where(r => r.Name == "Coordinator").Count() == 0)
            //{
            //    var newRole = new IdentityRole()
            //    {
            //        Name = "Coordinator"
            //    };

            //    var result = await _roleManager.CreateAsync(newRole);
            //}

            return View();
        }

        public IActionResult UserManagement()
        {
            var users = _userManager.Users;
            return View(users);
        }

        public IActionResult AddUser()
        {
            return View();
        }
    }
}
