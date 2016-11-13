using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PsychologyTest.Models
{
    public class PsyTestContext : IdentityDbContext<PsyTestUser>
    {
        private IConfigurationRoot config;

        public DbSet<Institucion> Instituciones { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<DeletedUsers> UsuariosViejos { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Psicologo> Psicologos { get; set; }

        public DbSet<PruebaPsicologica> PruebasPsicologicas{ get; set; }

        public DbSet<PreguntaAbierta> PreguntasAbiertas { get; set; }
        public DbSet<PreguntaDeOpcionMultiple> PreguntasDeOpcionMultiple { get; set; }

        public DbSet<Opcion> Opciones { get; set; }
        public DbSet<PruebaPsicologicaAsignada> PruebasPsicologicaAsignadas { get; set; }

        public DbSet<RespuestaAbierta> RespuestasAbiertas { get; set; }
        public DbSet<RespuestaCerrada> RespuestasCerradas { get; set; }
        public DbSet<MultiplesRespuestas> MultiplesRespuestas { get; set; }
        public DbSet<MultiplesRespuestasConValor> MultiplesRespuestasConValores { get; set; }
        public DbSet<OpcionEscogida> OpcionesEscogidas { get; set; }

        public DbSet<OpcionConValorDeVerdad> OpcionesConValorDeVerdad { get; set; }

        public PsyTestContext(IConfigurationRoot config, DbContextOptions options) 
            : base(options)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(config["ConnectionStrings:LocalConnection"]);
        }
    }
}