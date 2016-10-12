using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PsychologyTest.Controllers
{
    [Authorize(Roles = "Psy")]
    [Authorize("EmailConfirmedPolicy")]
    public class PsyController : Controller
    {
        #region Class Members
        public PsyController() { }
        #endregion

        #region Actions
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AsignTests()
        {
            return View();
        }

        public IActionResult ViewTests()
        {
            return View();
        }
        #endregion


    }
}
