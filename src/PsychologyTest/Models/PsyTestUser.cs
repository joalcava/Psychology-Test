﻿using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PsychologyTest.Models
{
    public class PsyTestUser : IdentityUser
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string TipoDocId { get; set; }
        public string DocId { get; set; }
        public string Genero { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string RolSolicitado { get; set; }
    }
}
