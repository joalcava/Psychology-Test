using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly UserManager<PsyTestUser> _userManager;
        private readonly SignInManager<PsyTestUser> _signInManager;
        private static bool _databaseChecked;
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

        public Models.IdType DocType(string type)
        {
            switch (type) {
                case "Cedula":
                    return Models.IdType.Cc;
                case "Tarjeta de identidad":
                    return Models.IdType.TarjetaIdentidad;
                case "Registro Civil":
                    return Models.IdType.RegistroCivil;
                case "Cedula extranjera":
                    return Models.IdType.CcExtrangera;
                case "Pasaporte":
                    return Models.IdType.Pasaporte;
                default:
                    return Models.IdType.Cc;
            }
        }

        public Models.Gender Gender(string gender)
        {
            switch (gender) {
                case "Masculino":
                    return Models.Gender.Masculino;
                case "Femenino":
                    return Models.Gender.Femenino;
                default:
                    return Models.Gender.Nodefinido;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(SignUpViewModel vm)
        {
            if (ModelState.IsValid) {
                var user = new PsyTestUser {
                    Nombres = vm.FirstName,
                    Apellidos = vm.LastName,
                    TipoDocId = DocType(vm.TipoDocId),
                    DocId = vm.DocId,
                    Genero = Gender(vm.Genero),
                    Direccion = vm.Direccion,
                    Activo = false,
                    FechaRegistro = DateTime.Now,
                    Email = vm.Correo,
                    UserName = vm.Correo
                };
                var result = await _userManager.CreateAsync(user, vm.Password);
                if (result.Succeeded) {
                    // TODO: Debe ser reemplazado con una pagina de espera por confirmacion del root
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, $"Nuevo usuario creado - {DateTime.Now}");
                    return RedirectToAction("Index", "App");
                }
                //AddErrors(result);
            }
            return View(vm);
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "App");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid) {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username,
                                                                            vm.Password,
                                                                            true, false);
                if (signInResult.Succeeded) {
                    if (string.IsNullOrWhiteSpace(returnUrl)) {
                        return RedirectToAction("Index", "App");
                    }
                    else {
                        return Redirect(returnUrl);
                    }
                }
                else {
                    ModelState.AddModelError("", "Contraseña o Email incorrectos");
                }
            }
            return View();
        }

        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated) {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "App");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult UserPage()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Signin()
        //{
        //    return View();
        //}
    }
}
