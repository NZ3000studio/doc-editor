using doc_editor.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace doc_editor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Editor()
        {
            var username = HttpContext.Session.GetString("Username");

            if (username == null)
            {
                return RedirectToAction("login", "Index");
            }

            
            try
            {
                DBhandler.LogConnection(username);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong.");
            }

            ViewBag.Username = username;
            return View();
        }
    }
}
