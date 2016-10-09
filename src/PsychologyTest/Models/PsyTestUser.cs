using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PsychologyTest.Models
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

    public class PsyTestUser : IdentityUser
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public IdType TipoDocId { get; set; }
        public string DocId { get; set; }
        public Gender Genero { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
