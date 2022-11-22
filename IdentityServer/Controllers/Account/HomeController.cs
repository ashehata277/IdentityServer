using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Talabatk.IDS.ViewModels;

namespace Talabatk.IDS.Controllers.Account
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger _logger;
        private readonly IConfiguration configuration;

        public HomeController(IIdentityServerInteractionService interaction,
                              IWebHostEnvironment environment,
                              ILogger<HomeController> logger,
                              IConfiguration configuration  )
        {
            _interaction = interaction;
            _environment = environment;
            _logger = logger;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {

          
                return View();

           
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;

               
                 
                    message.ErrorDescription = null;
            }

            return View("Error", vm);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult RedirectToadmin()
        {
            string domainurl = configuration.GetValue<string>("Old")+"/admin";
            return Redirect(domainurl);
        }
    }
}

