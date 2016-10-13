using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PsychologyTest.Models;
using PsychologyTest.ViewModels;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace PsychologyTest.Controllers
{
    public class AuthController : Controller
    {
        #region Class Members
        private readonly UserManager<PsyTestUser> _userManager;
        private readonly SignInManager<PsyTestUser> _signInManager;
        private readonly ILogger _logger;
        private IPsyTestRepository _context;

        public AuthController(
            UserManager<PsyTestUser> userManager,
            SignInManager<PsyTestUser> signInManager,
            ILoggerFactory loggerFactory,
            IPsyTestRepository context)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AuthController>();
            _context = context;
        }
        #endregion

        #region Actions
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name);
                return RedirectToAction("RedirectToRol", "Home", user);
            }
            var vm = new CreateUserViewModel();
            vm.Instituciones = _context.GetAllInstituciones();
            return View(vm);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(CreateUserViewModel vm)
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
            ViewBag.IntentoFallido = false;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                ViewBag.IntentoFallido = false;
                var loginResult = await _signInManager
                    .PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, false);

                if (loginResult.Succeeded) {
                    var user = await _userManager.FindByNameAsync(vm.Username);
                    return RedirectToAction("RedirectToRol", "Home", user);
                }
                else {
                    ModelState.AddModelError(string.Empty, "Contraseña o Email incorrectos");
                }
            }
            ViewBag.IntentoFallido = true;
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
        [HttpGet]
        public IActionResult ForgotPassword(string success)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name);
                return RedirectToAction("RedirectToRol", "Home");
            }
            ViewBag.Success = false;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ViewModels.AuthViewModels.ForgotPasswordViewModel vm){
            // Si ya esta logueado, se redirecciona al index de su rol;
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("RedirectToRol", "Home");

            if (ModelState.IsValid){
                var user = await _userManager.FindByNameAsync(vm.Email);
                try{
                    if (await _userManager.IsEmailConfirmedAsync(user)){
                        //TODO: IMPLEMENTAR SERVICIO DE EMAIL
                        //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                        //var callbackUrl = Url.Action("ResetPassword", "Auth", new {userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                        //await _emailSender.SendEmailAsync(vm.Email, "Reset Password",
                            //$"Para reiniciar su contraseña haga click aqui: <a href='{callbackUrl}'>link</a>");
                        vm.Email = string.Empty;
                        ViewBag.Success = true;
                        return View();
                    }
                }
                catch(ArgumentNullException) {
                    ModelState.AddModelError(string.Empty, "No se encontro a nadie registrado con ese correo o aun no a sido activado");
                }   
            }
            ViewBag.Success = false;
            return View(vm);
        }

        // GET: /Auth/ResetPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null) {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: /Auth/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ViewModels.AuthViewModels.ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null) {
                return RedirectToAction("ResetPasswordConfirmation", "Auth");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded) {
                return RedirectToAction("ResetPasswordConfirmation", "Auth");
            }
            foreach (var err in result.Errors) {
                ModelState.AddModelError(string.Empty, err.Description);
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        #endregion
    }
}