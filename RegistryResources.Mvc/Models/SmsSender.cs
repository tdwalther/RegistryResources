using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace RegistryResources.Mvc.Models
{
    public class SmsSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // throw new NotImplementedException();
            return null;
        }
    }
}
