using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PsychologyTest.Models;

namespace PsychologyTest.ViewModels.AdminViewModels
{
    public class CreateTestViewModel
    {
        public bool Success { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<Pregunta> Preguntas { get; set; }

    }
}
