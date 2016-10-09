using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyTest.ViewModels
{
    public enum IdType
    {
        Cc = 0,
        TarjetaIdentidad = 1,
        RegistroCivil = 2,
        CcExtrangera = 3,
        Pasaporte = 4,
    }

    public enum Gender
    {
        Masculino, Femenino, Nodefinido
    }

    public class SignUpViewModel
    {
        // Campos opcionales:
        public string LastName { get; set; }
        public string Direccion { get; set; }

        //Requeridos:
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Correo { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string DocId { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public string TipoDocId { get; set; }

        [Required]
        public string Genero { get; set; }

        [Required]
        public string Rol { get; set; }
    }
}
