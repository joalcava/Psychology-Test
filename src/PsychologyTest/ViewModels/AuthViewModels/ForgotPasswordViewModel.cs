using System.ComponentModel.DataAnnotations;

namespace PsychologyTest.ViewModels.AuthViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electronico")]
        public string Email { get; set; }
    }
}