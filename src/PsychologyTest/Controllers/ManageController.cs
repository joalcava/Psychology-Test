using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PsychologyTest.Models;

namespace PsychologyTest.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private UserManager<PsyTestUser> _userManager;

        public ManageController(UserManager<PsyTestUser> userManager) {
            this._userManager = userManager;
        }
        
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.Confirmed = user.EmailConfirmed;
            ViewData["UserName"] = user.Nombres;

            if (user.EmailConfirmed) {
                return View();
            }
            return View();
        }

        [Authorize("EmailConfirmedPolicy")]
        public IActionResult ChangePassword()
        {
            return View();
        }


        public async Task<IActionResult> WaitConfirmation()
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
            return View();
        }

    }
}
