using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PsychologyTest.Models;
// TODO: Loggin de errores y de informacion de acceso/modificacion de la base de datos
// TODO: Repositorio de la base de datos/ Manejador de la base de datos para hacer loggin
namespace PsychologyTest.Controllers
{
    public class AppController : Controller
    {
        private PsyTestContext _context;
        private ILogger<AppController> _logger;

        public AppController(
            PsyTestContext context,
            ILogger<AppController> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            var data = _context.Instituciones.ToList();
            return View();
        }
    }
}
