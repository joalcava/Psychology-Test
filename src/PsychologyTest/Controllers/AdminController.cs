using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult GroupsManage()
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            var model = _repository.GetAllGrupos();
            return View(model);
        }

        [HttpGet]
        public IActionResult InstitutionsManage()
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            var model = _repository.GetAllInstituciones();
            return View(model);
        }

        [HttpGet]
        public IActionResult TestManage()
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
                    vm.Success = true;
                    vm.Inst = "";
                    vm.Jornada = "";
                    vm.Grado = "";
                    vm.Nombre = "";
                    return View(vm);
                }
                ModelState.AddModelError("", "No se pudo crear el grupo");
            }
            vm.Success = false;
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
                    ViewData["Success"] = true;
                    return RedirectToAction("CreateInstitucion", "Admin");
                }
                ModelState.AddModelError("", "No se pudo crear la institucion");
            }
            ViewData["Success"] = false;
            return View(vm);
        }

        [HttpGet]
        public IActionResult EditGrupo(string grupoId)
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            Grupo model = _repository.GetGrupoById(Convert.ToInt32(grupoId));
            var viewModel = Mapper.Map<GrupoViewModel>(model);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditGrupo(GrupoViewModel vm)
        {
            // TODO: Implementar vista y controlador
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            ViewBag.Instituciones = _repository.GetAllInstitucionNames();
            if (ModelState.IsValid)
            {
                var newGrupoData = Mapper.Map<Grupo>(vm);
                _repository.UpdateGrupo(newGrupoData);
                ViewBag.Success = true;
                return View();
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult EditInstitucion(string instId)
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            Institucion model = _repository.GetInstitucionById(Convert.ToInt32(instId));
            var viewModel = Mapper.Map<InstitucionViewModel>(model);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditInstitucion(InstitucionViewModel vm)
        {
            // TODO: Implementar vista y controlador
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            if (ModelState.IsValid)
            {
                var newInstitucionData = Mapper.Map<Institucion>(vm);
                _repository.UpdateInstitucion(newInstitucionData);
                ViewBag.Success = true;
                return View();
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult DeleteGrupo(string grupoId)
        {
            bool result = _repository.DeleteGrupo(grupoId);
            if (result)
                return RedirectToAction("GroupsManage", "Admin");
            // TODO: Mostrar un mensaje de error en la vista
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult DeleteInstitucion(string instId)
        {
            bool result = _repository.DeleteInstitucion(instId);
            if (result)
                return RedirectToAction("InstitutionsManage", "Admin");
            // TODO: Mostrar un mensaje de error en la vista
            throw new NotImplementedException();
        }

        #endregion
    }
}
