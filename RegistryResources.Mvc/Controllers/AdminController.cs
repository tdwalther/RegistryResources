using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistryResources.Data;
using RegistryResources.Mvc.ViewModels;

namespace RegistryResources.Mvc.Controllers
{
    //[Authorize(Roles = "Administrator")]
    //[Authorize(Roles = "Coordinator")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDataContext _dataContext;

        public AdminController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IDataContext dataContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dataContext = dataContext;
        }
        public async Task<IActionResult> Index()
        {
            AdminViewModel model = new AdminViewModel();
            model.Patients = _dataContext.Patients.Include(p => p.Registrant).ThenInclude(p => p.Address).Include(p => p.Registrant.Email).Take(10);
            model.Proxies = _dataContext.Proxies.Include(p => p.Registrant).ThenInclude(p => p.Address).Include(p => p.Registrant.Email).Take(10);
            model.Researchers = _dataContext.Researchers.Include(p => p.Registrant).ThenInclude(p => p.Address).Include(p => p.Registrant.Email).Take(10);

            return View(model);
        }

        public async Task<IActionResult> UserManagement()
        {
            var users = _userManager.Users.ToList();

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

            //foreach (IdentityUser user in users)
            //{
            //    if( user.Email == "tdw1213@hotmail.com")
            //    {
            //        //var result = await _userManager.AddToRoleAsync(user, "Admin");
            //        //result = await _userManager.AddToRoleAsync(user, "Coordinator");
            //    }
            //    else if( _dataContext.Patients.Where(p=> p.Registrant.UserId == user.Id).Count() > 0)
            //    {
            //        var result = await _userManager.AddToRoleAsync(user, "Patient");
            //    }
            //    else if (_dataContext.Proxies.Where(p => p.Registrant.UserId == user.Id).Count() > 0)
            //    {
            //        var result = await _userManager.AddToRoleAsync(user, "Proxy");
            //    }
            //    else if (_dataContext.Researchers.Where(p => p.Registrant.UserId == user.Id).Count() > 0)
            //    {
            //        var result = await _userManager.AddToRoleAsync(user, "Researcher");
            //    }
            //}

            //foreach (IdentityUser user in users)
            //{
            //    try
            //    {
            //        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //        var result = await _userManager.ConfirmEmailAsync(user, token);
            //        //var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //        //var result = await _userManager.ResetPasswordAsync(user, token, "Eagles_123");
            //    }
            //    catch (Exception ex)
            //    {
            //        string msg = ex.Message;
            //    }
            //}

            //var researchers = _dataContext.Proxies
            //    .Include(r => r.Registrant)
            //    .ThenInclude(rg => rg.Email)
            //    .ToList();

            //foreach (var reg in researchers)
            //{
            //    if (string.IsNullOrEmpty(reg.Registrant.UserId))
            //    {
            //        var user = new IdentityUser()
            //        {
            //            UserName = reg.Registrant.Email.EmailAddress,
            //            Email = reg.Registrant.Email.EmailAddress
            //        };

            //        var result = await _userManager.CreateAsync(user, reg.Registrant.FirstName + "_" + reg.Registrant.LastName + "_123");

            //        if( result.Succeeded)
            //        {
            //            var user2 = await _userManager.FindByNameAsync(user.UserName);
            //            var id = user2.Id;
            //            reg.Registrant.UserId = id;
            //        }
            //    }
            //}

            //_dataContext.Save();



            return View(users);
        }

        public IActionResult AddUser()
        {
            return View();
        }
    }
}
