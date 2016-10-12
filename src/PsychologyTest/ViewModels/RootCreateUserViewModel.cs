using System;
using System.ComponentModel.DataAnnotations;

namespace PsychologyTest.ViewModels
{
    public class RootCreateUserViewModel
    {
        [StringLength(25, MinimumLength = 4, ErrorMessage = "El nombre debe ser de almenos 4 caracteres y maximo 25")]
        [Display(Name = "Nombres")]
        public string Nombres { get; set; }

        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo electronico")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(25, MinimumLength = 8)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        [Display(Name = "Confirme la contraseña")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Tipo de documento")]
        public string TipoDocId { get; set; }

        [StringLength(25, MinimumLength = 4, ErrorMessage = "La identificacion no debe ser menor de 4 caracteres ni mayor de 25")]
        [Display(Name = "Numero de documento")]
        public string DocId { get; set; }

        public string Direccion { get; set; }

        public string PhoneNumber { get; set; }

        public string Genero { get; set; }

        [Required]
        public string RolSolicitado { get; set; }

        [Display(Name = "Confirmar usuario?")]
        public bool EmailConfirmed { get; set; }
    }
}
