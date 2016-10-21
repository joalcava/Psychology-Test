using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PsychologyTest.Models;
using PsychologyTest.ViewModels;

namespace PsychologyTest.Controllers
{
    [Authorize(Roles = "Root")]
    [Authorize("EmailConfirmedPolicy")]
    public class RootController : Controller
    {
        #region Class Members
        private UserManager<PsyTestUser> _userManager;
        private IPsyTestRepository _repository;

        public RootController(IPsyTestRepository repository, UserManager<PsyTestUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        #endregion

        #region Actions
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UsersManage()
        {
            ViewBag.NoConfirmedUsers = _repository.GetAllUnconfirmedUsers();
            ViewBag.ThereIsNoConfirmedUsers =
                (ViewBag.NoConfirmedUsers as IEnumerable<PsyTestUser>).Any() ? true : false;
            var model = _repository.GetAllConfirmedUsers();
            return View(model);
        }

        [Route("Root/ConfirmUser")]
        public async Task<IActionResult> ConfirmUser(string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                string rol = Rol(user.RolSolicitado);
                await _userManager.AddToRoleAsync(user, rol);
                await _userManager.AddClaimAsync(user, new Claim("emailconfirmation", "1"));
                _repository.ConfirmEmail(user);
                return RedirectToAction("UsersManage", "Root");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("No se mando el correo");
                return RedirectToAction("UsersManage", "Root");
            }
        }

        public IActionResult CreateUser(bool userCreated)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RootCreateUserViewModel vm)
        {
            if (ModelState.IsValid) {
                var newUser = Mapper.Map<PsyTestUser>(vm);
                newUser.UserName = newUser.Email;
                newUser.Activo = true;
                newUser.FechaRegistro = DateTime.Now;
                var result = await _userManager.CreateAsync(newUser, vm.Password);
                if (result.Succeeded) {
                    await _userManager.AddClaimAsync(newUser, new Claim("active", "1"));
                    ViewBag.userCreated = true;
                    if (newUser.EmailConfirmed) {
                        string rol = Rol(vm.RolSolicitado);
                        await _userManager.AddToRoleAsync(newUser, rol);
                        await _userManager.AddClaimAsync(newUser, new Claim("emailconfirmation", "1"));
                        return View();
                    }
                    return View();
                }
                foreach (var err in result.Errors)
                    ModelState.AddModelError("", err.Description);
                }
            ViewBag.userCreated = false;
            return View(vm);
        }

        public async Task<IActionResult> DenyUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            _repository.DeleteUserPerm(user);
            return RedirectToAction("UsersManage", "Root");
        }

        public async Task<IActionResult> DeleteUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            _repository.DeleteUser(user);
            return RedirectToAction("UsersManage", "Root");
        }

        public async Task<IActionResult> EditUser(string userName)
        {
            var model = await _userManager.FindByNameAsync(userName);
            var modelReversed = Mapper.Map<RootEditUserViewModel>(model);
            return View(modelReversed);
        }

        [HttpPost]
        public IActionResult EditUser(RootEditUserViewModel vm)
        {
            if (ModelState.IsValid) {
                var newUserData = Mapper.Map<PsyTestUser>(vm);
                _repository.UpdateUser(vm.Email, newUserData);
                return RedirectToAction("UsersManage", "Root");
            }
            return View(vm);
        }
        #endregion

        private string Rol(string r)
        {
            switch (r) {
                case "Administrador":
                    return "Admin";
                case "Psicologo":
                    return "Psy";
                case "Estudiante":
                    return "Stud";
                case "Root":
                    return "Root";
                default:
                    return null;
            }
        }

    }
}
