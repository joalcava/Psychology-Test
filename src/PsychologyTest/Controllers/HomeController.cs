using Microsoft.AspNetCore.Mvc;

namespace PsychologyTest.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public IActionResult Index() {
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "App");
            }

            return View();
        }

        public IActionResult About() {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact() {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        public IActionResult Error() {
            return View();
        }
    }
}
