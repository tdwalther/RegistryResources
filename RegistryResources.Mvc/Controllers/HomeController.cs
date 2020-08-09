using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RegistryResources.Data;
using RegistryResources.Mvc.Models;

namespace RegistryResources.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataContext _dataContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public HomeController(
            ILogger<HomeController> logger,
            IDataContext dataContext,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _dataContext = dataContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Coordinator"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (User.IsInRole("Patient") )
                {
                    return RedirectToAction("Index", "Patient");
                }
                else if( User.IsInRole("Proxy"))
                {
                    return RedirectToAction("Index", "Proxy");
                }
                else if(User.IsInRole("Researcher"))
                {
                    return RedirectToAction("Index", "Researcher");
                }
                else
                {
                    return RedirectToAction("Index", "Patient");
                }
            }
            else
            {
                var patients = _dataContext.Patients.Count();
                var researchers = _dataContext.Researchers.Count();
                var studies = _dataContext.Surveys.Count();
                var questions = _dataContext.Questions.Count();
                var answers = _dataContext.Answers.Count();

                MetricsViewModel model = new MetricsViewModel();
                model.Counters.Add(new Tuple<string, int>("PatientsRegistered", patients));
                model.Counters.Add(new Tuple<string, int>("ResearchersRegistered", researchers));
                model.Counters.Add(new Tuple<string, int>("CompletedStudies", studies));
                model.Counters.Add(new Tuple<string, int>("QuestionsAsked", questions));
                model.Counters.Add(new Tuple<string, int>("AnswersGiven", answers));

                return View(model);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
