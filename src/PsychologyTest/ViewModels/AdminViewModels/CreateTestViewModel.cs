using System.ComponentModel.DataAnnotations;

namespace PsychologyTest.ViewModels.AdminViewModels
{
    public class CreateTestViewModel
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "El nombre debe contener almenos 4 caracteres y maximo 30")]
        [Display(Name = "*Agregue un titulo o nombre a la prueba")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "*Agregue una descripcion a la prueba")]
        [StringLength(10000, MinimumLength = 20, ErrorMessage = "La descripcion debe contener almenos 20 caracteres y maximo 10000, recuerde que se acepta el uso de Markdown")]
        public string Descripcion { get; set; }
    }
}
