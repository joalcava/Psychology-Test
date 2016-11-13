using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PsychologyTest.Models;

namespace PsychologyTest.Migrations
{
    [DbContext(typeof(PsyTestContext))]
    partial class PsyTestContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PsychologyTest.Models.DeletedUsers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Activo");

                    b.Property<string>("Apellidos");

                    b.Property<string>("Direccion");

                    b.Property<string>("DocId");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<DateTime>("FechaNacimiento");

                    b.Property<DateTime>("FechaRegistro");

                    b.Property<string>("Genero");

                    b.Property<string>("Nombres");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("RolSolicitado");

                    b.Property<string>("TipoDocId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("UsuariosViejos");
                });

            modelBuilder.Entity("PsychologyTest.Models.Estudiante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GrupoId");

                    b.Property<int?>("InstitucionNit");

                    b.Property<string>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("GrupoId");

                    b.HasIndex("InstitucionNit");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Estudiantes");
                });

            modelBuilder.Entity("PsychologyTest.Models.Grupo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Grado");

                    b.Property<int?>("InstitucionNit");

                    b.Property<string>("Jornada");

                    b.Property<string>("Nombre");

                    b.Property<int?>("PsicologoId");

                    b.HasKey("Id");

                    b.HasIndex("InstitucionNit");

                    b.HasIndex("PsicologoId");

                    b.ToTable("Grupos");
                });

            modelBuilder.Entity("PsychologyTest.Models.Institucion", b =>
                {
                    b.Property<int>("Nit");

                    b.Property<string>("Ciudad");

                    b.Property<string>("Direccion");

                    b.Property<string>("Nombre");

                    b.Property<string>("SitioWeb");

                    b.Property<string>("Telefono");

                    b.HasKey("Nit");

                    b.ToTable("Instituciones");
                });

            modelBuilder.Entity("PsychologyTest.Models.Opcion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PreguntaDeOpcionMultipleId");

                    b.Property<string>("Texto");

                    b.Property<int>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("PreguntaDeOpcionMultipleId");

                    b.ToTable("Opciones");
                });

            modelBuilder.Entity("PsychologyTest.Models.OpcionConValorDeVerdad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MultiplesRespuestasConValorId");

                    b.Property<int>("Opcion");

                    b.Property<int>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("MultiplesRespuestasConValorId");

                    b.ToTable("OpcionesConValorDeVerdad");
                });

            modelBuilder.Entity("PsychologyTest.Models.OpcionEscogida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MultiplesRespuestasId");

                    b.Property<int>("Opcion");

                    b.HasKey("Id");

                    b.HasIndex("MultiplesRespuestasId");

                    b.ToTable("OpcionesEscogidas");
                });

            modelBuilder.Entity("PsychologyTest.Models.Pregunta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Anotaciones");

                    b.Property<string>("Descripcion");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("Posicion");

                    b.Property<int?>("PruebaPsicologicaId");

                    b.HasKey("Id");

                    b.HasIndex("PruebaPsicologicaId");

                    b.ToTable("Pregunta");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Pregunta");
                });

            modelBuilder.Entity("PsychologyTest.Models.PruebaPsicologica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Activo");

                    b.Property<string>("Descripcion");

                    b.Property<DateTime>("FechaCreado");

                    b.Property<DateTime>("FechaModificado");

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("PruebasPsicologicas");
                });

            modelBuilder.Entity("PsychologyTest.Models.PruebaPsicologicaAsignada", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Completado");

                    b.Property<string>("Diagnostico");

                    b.Property<int?>("EncuestadoId");

                    b.Property<DateTime>("FechaAsignacion");

                    b.Property<DateTime>("FechaFinalizacion");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<bool>("Iniciado");

                    b.Property<int?>("PruebaAsignadaId");

                    b.HasKey("Id");

                    b.HasIndex("EncuestadoId");

                    b.HasIndex("PruebaAsignadaId");

                    b.ToTable("PruebasPsicologicaAsignadas");
                });

            modelBuilder.Entity("PsychologyTest.Models.Psicologo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Psicologos");
                });

            modelBuilder.Entity("PsychologyTest.Models.PsyTestUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<bool>("Activo");

                    b.Property<string>("Apellidos");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Direccion");

                    b.Property<string>("DocId");

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<DateTime>("FechaRegistro");

                    b.Property<string>("Genero");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Nombres");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("RolSolicitado");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("TipoDocId");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("PsychologyTest.Models.Respuesta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("FechaRespondida");

                    b.Property<int?>("PreguntaId");

                    b.Property<int?>("PruebaPsicologicaAsignadaId");

                    b.HasKey("Id");

                    b.HasIndex("PreguntaId");

                    b.HasIndex("PruebaPsicologicaAsignadaId");

                    b.ToTable("Respuesta");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Respuesta");
                });

            modelBuilder.Entity("PsychologyTest.Models.PreguntaAbierta", b =>
                {
                    b.HasBaseType("PsychologyTest.Models.Pregunta");

                    b.Property<bool>("Larga");

                    b.ToTable("PreguntaAbierta");

                    b.HasDiscriminator().HasValue("PreguntaAbierta");
                });

            modelBuilder.Entity("PsychologyTest.Models.PreguntaDeOpcionMultiple", b =>
                {
                    b.HasBaseType("PsychologyTest.Models.Pregunta");

                    b.Property<bool>("MultiplesRespuestas");

                    b.Property<bool>("RespuestaConValorDeVerdad");

                    b.ToTable("PreguntaDeOpcionMultiple");

                    b.HasDiscriminator().HasValue("PreguntaDeOpcionMultiple");
                });

            modelBuilder.Entity("PsychologyTest.Models.MultiplesRespuestas", b =>
                {
                    b.HasBaseType("PsychologyTest.Models.Respuesta");


                    b.ToTable("MultiplesRespuestas");

                    b.HasDiscriminator().HasValue("MultiplesRespuestas");
                });

            modelBuilder.Entity("PsychologyTest.Models.MultiplesRespuestasConValor", b =>
                {
                    b.HasBaseType("PsychologyTest.Models.Respuesta");


                    b.ToTable("MultiplesRespuestasConValor");

                    b.HasDiscriminator().HasValue("MultiplesRespuestasConValor");
                });

            modelBuilder.Entity("PsychologyTest.Models.RespuestaAbierta", b =>
                {
                    b.HasBaseType("PsychologyTest.Models.Respuesta");

                    b.Property<string>("TextoRespuesta");

                    b.ToTable("RespuestaAbierta");

                    b.HasDiscriminator().HasValue("RespuestaAbierta");
                });

            modelBuilder.Entity("PsychologyTest.Models.RespuestaCerrada", b =>
                {
                    b.HasBaseType("PsychologyTest.Models.Respuesta");

                    b.Property<int>("OpcionRespuesta");

                    b.ToTable("RespuestaCerrada");

                    b.HasDiscriminator().HasValue("RespuestaCerrada");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PsychologyTest.Models.PsyTestUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PsychologyTest.Models.PsyTestUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PsychologyTest.Models.PsyTestUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PsychologyTest.Models.Estudiante", b =>
                {
                    b.HasOne("PsychologyTest.Models.Grupo", "Grupo")
                        .WithMany()
                        .HasForeignKey("GrupoId");

                    b.HasOne("PsychologyTest.Models.Institucion", "Institucion")
                        .WithMany()
                        .HasForeignKey("InstitucionNit");

                    b.HasOne("PsychologyTest.Models.PsyTestUser", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("PsychologyTest.Models.Grupo", b =>
                {
                    b.HasOne("PsychologyTest.Models.Institucion", "Institucion")
                        .WithMany("Grupos")
                        .HasForeignKey("InstitucionNit");

                    b.HasOne("PsychologyTest.Models.Psicologo")
                        .WithMany("Grupos")
                        .HasForeignKey("PsicologoId");
                });

            modelBuilder.Entity("PsychologyTest.Models.Opcion", b =>
                {
                    b.HasOne("PsychologyTest.Models.PreguntaDeOpcionMultiple")
                        .WithMany("Opciones")
                        .HasForeignKey("PreguntaDeOpcionMultipleId");
                });

            modelBuilder.Entity("PsychologyTest.Models.OpcionConValorDeVerdad", b =>
                {
                    b.HasOne("PsychologyTest.Models.MultiplesRespuestasConValor")
                        .WithMany("Respuestas")
                        .HasForeignKey("MultiplesRespuestasConValorId");
                });

            modelBuilder.Entity("PsychologyTest.Models.OpcionEscogida", b =>
                {
                    b.HasOne("PsychologyTest.Models.MultiplesRespuestas")
                        .WithMany("Respuestas")
                        .HasForeignKey("MultiplesRespuestasId");
                });

            modelBuilder.Entity("PsychologyTest.Models.Pregunta", b =>
                {
                    b.HasOne("PsychologyTest.Models.PruebaPsicologica")
                        .WithMany("Preguntas")
                        .HasForeignKey("PruebaPsicologicaId");
                });

            modelBuilder.Entity("PsychologyTest.Models.PruebaPsicologicaAsignada", b =>
                {
                    b.HasOne("PsychologyTest.Models.Estudiante", "Encuestado")
                        .WithMany()
                        .HasForeignKey("EncuestadoId");

                    b.HasOne("PsychologyTest.Models.PruebaPsicologica", "PruebaAsignada")
                        .WithMany()
                        .HasForeignKey("PruebaAsignadaId");
                });

            modelBuilder.Entity("PsychologyTest.Models.Psicologo", b =>
                {
                    b.HasOne("PsychologyTest.Models.PsyTestUser", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("PsychologyTest.Models.Respuesta", b =>
                {
                    b.HasOne("PsychologyTest.Models.Pregunta", "Pregunta")
                        .WithMany()
                        .HasForeignKey("PreguntaId");

                    b.HasOne("PsychologyTest.Models.PruebaPsicologicaAsignada")
                        .WithMany("Respuestas")
                        .HasForeignKey("PruebaPsicologicaAsignadaId");
                });
        }
    }
}
