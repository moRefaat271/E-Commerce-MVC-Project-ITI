using Identity.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace Identity.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
            {
                // Handle file not found exception

            }
            else if (exceptionHandlerPathFeature?.Path == "/index")
            {
                // Handle exceptions from /index route
            }

            _logger.LogError($"An error occurred: {exceptionHandlerPathFeature?.Error}");

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("Home/Error/404")]
        public IActionResult Error404()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {

            return View();
        }



    }
}