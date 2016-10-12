using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PsychologyTest.Models;
using PsychologyTest.ViewModels;
using Microsoft.Extensions.Logging;

namespace PsychologyTest.Controllers
{
    public class AuthController : Controller
    {
        #region Class Members
        private readonly UserManager<PsyTestUser> _userManager;
        private readonly SignInManager<PsyTestUser> _signInManager;
        private readonly ILogger _logger;

        public AuthController(
            UserManager<PsyTestUser> userManager,
            SignInManager<PsyTestUser> signInManager,
            ILoggerFactory loggerFactory)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AuthController>();
        }
        #endregion

        #region Actions
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name);
                return RedirectToAction("RedirectToRol", "Home", user);
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid) {
                var newUser = Mapper.Map<PsyTestUser>(vm);
                newUser.UserName = newUser.Email;
                newUser.FechaRegistro = DateTime.Now;
                newUser.Activo = true;
                var result = await _userManager.CreateAsync(newUser, vm.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    _logger.LogInformation(3, $"Nuevo usuario creado - {DateTime.Now}");
                    return RedirectToAction("WaitConfirmation", "Manage");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(vm);
        }

        [AllowAnonymous]
        public IActionResult SignUpCancel()
        {
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) {
                var user = _userManager.FindByNameAsync(User.Identity.Name);
                return RedirectToAction("RedirectToRol", "Home", user);
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager
                    .PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, false);

                if (signInResult.Succeeded) {
                    var user = await _userManager.FindByNameAsync(vm.Username);
                    return RedirectToAction("RedirectToRol", "Home", user);
                }
                else {
                    ModelState.AddModelError("", "Contraseña o Email incorrectos");
                }
            }
            return View();
        }

        [Authorize]
        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name);
                return RedirectToAction("RedirectToRol", "Home", user);
            }
            return View();
        }
        #endregion
    }
}
