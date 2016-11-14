using System.ComponentModel.DataAnnotations;

namespace PsychologyTest.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }
    }
}