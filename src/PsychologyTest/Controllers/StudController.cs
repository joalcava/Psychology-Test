using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PsychologyTest.Controllers
{
    [Authorize(Roles = "Stud")]
    [Authorize("EmailConfirmedPolicy")]
    public class StudController : Controller
    {
        #region Class Memebers
        public StudController()
        {
            
        }
        #endregion


        #region Actions
        public IActionResult Index()
        {
            return View();
        }
        #endregion
    }


}
