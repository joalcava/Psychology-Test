using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PsychologyTest.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
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

        [Required]
        [Display(Name = "Tipo de documento")]
        public string TipoDocId { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 4, ErrorMessage = "Su identificacion no debe ser menor de 4 caracteres ni mayor de 25")]
        [Display(Name = "Numero de documento")]
        public string DocId { get; set; }

        [Display(Name = "Numero de telefono")]
        public string PhoneNumber { get; set; }

        public string Direccion { get; set; }

        [Required]
        public string Genero { get; set; }

        [Required]
        public string RolSolicitado { get; set; }

        public string InstitucionSolicitada { get; set; }
        public string GrupoSolicitado { get; set; }

        public IEnumerable<Models.Institucion> Instituciones { get; set; }
    }
}
