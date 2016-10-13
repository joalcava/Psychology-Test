using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PsychologyTest.Models;

namespace PsychologyTest.Controllers
{
    public class HomeController : Controller
    {
        #region Class Members
        private UserManager<PsyTestUser> _userManager;

        public HomeController(UserManager<PsyTestUser> userManager)
        {
            _userManager = userManager;
        }
        #endregion

        #region Actions
        [Authorize]
        public async Task<IActionResult> RedirectToRol()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (await _userManager.IsEmailConfirmedAsync(user))
            {
                if (await _userManager.IsInRoleAsync(user, "Stud"))
                    return RedirectToAction("Index", "Stud");
                if (await _userManager.IsInRoleAsync(user, "Psy"))
                    return RedirectToAction("Index", "Psy");
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                    return RedirectToAction("Index", "Admin");
                if (await _userManager.IsInRoleAsync(user, "Root"))
                    return RedirectToAction("Index", "Root");
            }
            return RedirectToAction("WaitConfirmation", "Manage");
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated) {
                var user = _userManager.FindByNameAsync(User.Identity.Name);
                return RedirectToAction("RedirectToRol", "Home", user);
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Psychology Test description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "MasterTeam contact page.";
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        #endregion
    }
}
