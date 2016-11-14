using System;
using System.ComponentModel.DataAnnotations;

namespace PsychologyTest.ViewModels
{
    public class InstitucionViewModel
    {
        public bool Success { get; set; }

        public int instId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Nit { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "El nombre debe contener almenos cuatro caracteres y maximo 50")]
        public string Nombre { get; set; }

        public string Direccion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "El telefono debe contener almenos cuatro caracteres y maximo 20")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "La ciudad debe contener almenos cuatro caracteres y maximo 20")]
        public string Ciudad { get; set; }

        public string SitioWeb { get; set; }
    }
}