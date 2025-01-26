using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

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
           

            if ((username == "test1" && password == "test1715")|| ("test2" == username && password == "test1935"))
            {
                HttpContext.Session.SetString("Username", username);
                return RedirectToAction("Editor", "Home");
            }
            ViewBag.ErrorMessage = "Invalid username or password";

            return View("Index");
        }
    }
}
