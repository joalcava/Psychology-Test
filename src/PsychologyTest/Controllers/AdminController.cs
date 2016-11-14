using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PsychologyTest.Models;
using PsychologyTest.ViewModels;
using PsychologyTest.ViewModels.AdminViewModels;

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
            if (ViewBag.UserRol == "Root")
                return RedirectToAction("Index", "Root");
            return View();
        }

        #region Instituciones
        [HttpGet]
        public IActionResult InstitutionsManage()
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            var model = _repository.GetAllInstituciones();
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateInstitucion()
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            return View(new InstitucionViewModel());
        }

        [HttpPost]
        public IActionResult CreateInstitucion(InstitucionViewModel vm)
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            if (ModelState.IsValid)
            {
                bool result = _repository.AddInstitucion(Mapper.Map<Institucion>(vm));
                if (result)
                {
                    return View(new InstitucionViewModel { Success = true });
                }
                ModelState.AddModelError("", "No se pudo crear la institucion");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult EditInstitucion(int instId)
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            Institucion model = _repository.GetInstitucionById(instId);
            var viewModel = Mapper.Map<InstitucionViewModel>(model);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditInstitucion(InstitucionViewModel vm)
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            if (ModelState.IsValid)
            {
                var newInstitucionData = Mapper.Map<Institucion>(vm);
                _repository.UpdateInstitucion(newInstitucionData);
                vm.Success = true;
                return View(vm);
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult DeleteInstitucion(int instId)
        {
            bool result = _repository.DeleteInstitucion(instId);
            if (result)
                return RedirectToAction("InstitutionsManage", "Admin");
            // TODO: Mostrar un mensaje de error en la vista
            throw new NotImplementedException();
        }
        #endregion

        #region Grupos
        [HttpGet]
        public IActionResult GroupsManage()
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            var model = _repository.GetAllGrupos();
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateGrupo()
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            GrupoViewModel vm = new GrupoViewModel();
            vm.ListadoInstituciones = _repository.GetAllInstitucionNames();
            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateGrupo(GrupoViewModel vm)
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            if (ModelState.IsValid)
            {
                bool result = _repository.AddGrupo(vm);
                if (result)
                {
                    GrupoViewModel viewModel = new GrupoViewModel
                    {
                        Success = true,
                        ListadoInstituciones = _repository.GetAllInstitucionNames()
                    };
                    return View(viewModel);
                }
                ModelState.AddModelError(string.Empty, "No se pudo crear el grupo");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult EditGrupo(string grupoId)
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            Grupo model = _repository.GetGrupoById(Convert.ToInt32(grupoId));
            var vm = Mapper.Map<GrupoViewModel>(model);
            vm.ListadoInstituciones = _repository.GetAllInstitucionNames();
            vm.Inst = model.Institucion.Nombre;
            return View(vm);
        }

        [HttpPost]
        public IActionResult EditGrupo(GrupoViewModel vm)
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            if (ModelState.IsValid)
            {
                var newGrupoData = Mapper.Map<Grupo>(vm);
                _repository.UpdateGrupo(newGrupoData);
                vm.ListadoInstituciones = _repository.GetAllInstitucionNames();
                vm.Success = true;
                return View(vm);
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
        #endregion

        #region Pruebas
        [HttpGet]
        public IActionResult TestManage()
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            var vm = _repository.GetAllTests(include: true);
            return View(vm);
        }

        [HttpGet]
        public IActionResult CreateTest()
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            return View(new CreateTestViewModel());
        }

        [HttpPost]
        public IActionResult CreateTest(CreateTestViewModel vm)
        {
            if (ModelState.IsValid) {
                var newTest = new PruebaPsicologica
                {
                    Nombre = vm.Nombre,
                    Descripcion = vm.Descripcion,
                    Preguntas = new List<Pregunta>(),
                    FechaCreado = DateTime.Now,
                    FechaModificado = DateTime.MinValue,
                    Activo = false
                };
                var result = _repository.AddTest(newTest);
                if (result != null)
                {
                    var responsevm = new AddQuestionsToTestViewModel {Id = result.Id, Nombre = result.Nombre};
                    return RedirectToAction("AddQuestionsToTest", "Admin", responsevm);
                }
                ModelState.AddModelError("", "Hubo un problema mientras se intentaba agregar el nuevo test, porfavor intente mas tarde.");
                return View(vm);
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult AddQuestionsToTest(AddQuestionsToTestViewModel vm)
        {
            ViewBag.UserRol = User.IsInRole("Root") ? "Root" : "Admin";
            return View(vm);
        }

        [HttpGet]
        public IActionResult DeleteTest(int testId)
        {
            _repository.DeleteTest(testId);
            return RedirectToAction("TestManage", "Admin");
        }

        [HttpGet]
        public IActionResult EditTest(int testId)
        {
            return View();
        }

        #endregion

        #endregion
    }
}
