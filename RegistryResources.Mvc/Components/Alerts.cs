using Microsoft.AspNetCore.Mvc;
using RegistryResources.Business;
using RegistryResources.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Identity.Core;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace RegistryResources.Mvc.Components
{
    public class Alerts : ViewComponent
    {            
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IDataContext _dataContext;

        public Alerts(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IDataContext dataContext)       
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dataContext = dataContext;
        }

        public IViewComponentResult Invoke()
        {
            var task = _userManager.FindByEmailAsync(User.Identity.Name);
            task.Wait();
            var userId = task.Result.Id;  //.FindFirstValue(ClaimTypes.NameIdentifier);

            var alerts = _dataContext.Alerts.Where(a => a.UserId == userId).ToList();

            List<AlertItem> AlertItems = new List<AlertItem>();
            AlertItems.AddRange(alerts.Select(a=> new AlertItem() { AlertMessage = a.Message }));
            return View(AlertItems);
        }
    }

    public class AlertItem
    {
        public string AlertMessage { get; set; }

    }
}
