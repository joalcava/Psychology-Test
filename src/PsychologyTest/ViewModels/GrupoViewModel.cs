using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PsychologyTest.ViewModels
{
    public class GrupoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "El nombre debe contener almenos dos caracteres y maximo 25")]
        public string Nombre { get; set; }

        public string Grado { get; set; }

        public string Jornada { get; set; }

        [Display(Name = "Institucion")]
        public string Inst { get; set; }

        public bool Success { get; set; } = false;

        public List<string> ListadoInstituciones { get; set; }

    }
}
