using System.ComponentModel.DataAnnotations;

namespace PsychologyTest.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Recuerdame")]
        public bool RememberMe { get; set; }
    }
}
