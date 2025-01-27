using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using BCrypt.Net;

namespace doc_editor.Controllers
{
    public class LoginController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult login(string username, string password)
        {
            string storedHash1 = "$2b$10$1g2whZmMbpIpT1tyMBOZjes6DaNDKmRmNXIaZFUb29y6GWwFhME3K";
            string storedHash2 = "$2b$10$InNxnOJ1Zsq1RPgurCgwvuAcahyI4n4fKvqJ.McvjYdIyx6QlkkLG";

            if ((username == "test1" && BCrypt.Net.BCrypt.Verify(password, storedHash1)) || (username == "test2" && BCrypt.Net.BCrypt.Verify(password, storedHash2)))
            {
                HttpContext.Session.SetString("Username", username);
                return RedirectToAction("Editor", "Home");
            }
            ViewBag.ErrorMessage = "Invalid username or password";

            return View("Index");
        }
    }
}
