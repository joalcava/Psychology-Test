using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PsychologyTest.Models;
using PsychologyTest.ViewModels;

namespace PsychologyTest.Controllers
{
    [Authorize(Roles = "Admin,Root")]
    [Authorize("EmailConfirmedPolicy")]
    public class AdminController : Controller
    {
        #region Class Members
        private IPsyTestRepository _repository;

        public AdminController(IPsyTestRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Actions
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GroupsManage()
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            var model = _repository.GetAllGrupos();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> InstitutionsManage()
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            var model = _repository.GetAllInstituciones();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> TestManage()
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            return View();
        }

        [HttpGet]
        public IActionResult CreateGrupo()
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            ViewBag.Instituciones = _repository.GetAllInstitucionNames();
            return View();
        }

        [HttpPost]
        public IActionResult CreateGrupo(GrupoViewModel vm)
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            ViewBag.Instituciones = _repository.GetAllInstitucionNames();
            if (ModelState.IsValid) {
                bool result = _repository.AddGrupo(vm);
                if (result) {
                    return View();
                }
                ModelState.AddModelError("", "No se pudo crear el grupo");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult CreateInstitucion()
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            return View();
        }

        [HttpPost]
        public IActionResult CreateInstitucion(InstitucionViewModel vm)
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            if (ModelState.IsValid) {
                bool result = _repository.AddInstitucion(Mapper.Map<Institucion>(vm));
                if (result) {
                    return View();
                }
                ModelState.AddModelError("", "No se pudo crear la institucion");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult EditGrupo()
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            return View();
        }

        [HttpPost]
        public IActionResult EditGrupo(GrupoViewModel vm)
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            ViewBag.Instituciones = _repository.GetAllInstitucionNames();
            if (ModelState.IsValid)
            {

            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult EditInstitucion()
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            return View();
        }

        [HttpPost]
        public IActionResult EditInstitucion(InstitucionViewModel vm)
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            if (ModelState.IsValid)
            {

            }
            return View(vm);
        }
        #endregion
    }
}
