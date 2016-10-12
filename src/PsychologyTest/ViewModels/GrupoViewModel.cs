using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyTest.ViewModels
{
    public class GrupoViewModel
    {
        [Required]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "El nombre debe contener almenos dos caracteres y maximo 25")]
        public string Nombre { get; set; }

        public string Grado { get; set; }

        public string Jornada { get; set; }

        [Display(Name = "Institucion")]
        public string Inst { get; set; }
    }
}
