﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistryResources.Business;
using RegistryResources.Data;

namespace RegistryResources.Mvc.Controllers
{
    public class PatientController : Controller
    {
        private readonly IDataContext _dataContext;
        private readonly UserManager<IdentityUser> _userManager;

        public PatientController(
            IDataContext dataContext,
             UserManager<IdentityUser> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserName = User.Identity.Name;
            ViewBag.UserID = userId;

            PatientModel patient = _dataContext.Patients
                .Include(p=> p.Registrant)
                .ThenInclude(p=> p.Address)
                .Where(p => p.Registrant.UserId == userId).FirstOrDefault();

            return View(patient);
        }
    }
}
