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
        public DbSet<Pregunta> Preguntas { get; set; }
        public DbSet<TipoPregunta> TiposDePregunta{ get; set; }
        public DbSet<OpcionMultiple> OpcionesMultiples{ get; set; }
        public DbSet<PruebaAsignada> PruebasAsignadas{ get; set; }


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