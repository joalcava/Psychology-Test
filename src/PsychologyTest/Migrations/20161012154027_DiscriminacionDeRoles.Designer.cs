using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PsychologyTest.Models;

namespace PsychologyTest.Migrations
{
    [DbContext(typeof(PsyTestContext))]
    [Migration("20161012154027_DiscriminacionDeRoles")]
    partial class DiscriminacionDeRoles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int?>("InstitucionId");

                    b.Property<string>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("GrupoId");

                    b.HasIndex("InstitucionId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Estudiantes");
                });

            modelBuilder.Entity("PsychologyTest.Models.Grupo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Grado");

                    b.Property<int?>("InstitucionId");

                    b.Property<string>("Jornada");

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.HasIndex("InstitucionId");

                    b.ToTable("Grupos");
                });

            modelBuilder.Entity("PsychologyTest.Models.Institucion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Ciudad");

                    b.Property<string>("Direccion");

                    b.Property<string>("Nit");

                    b.Property<string>("Nombre");

                    b.Property<string>("SitioWeb");

                    b.Property<string>("Telefono");

                    b.HasKey("Id");

                    b.ToTable("Instituciones");
                });

            modelBuilder.Entity("PsychologyTest.Models.Psicologo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GrupoId");

                    b.Property<int?>("InstitucionId");

                    b.Property<string>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("GrupoId");

                    b.HasIndex("InstitucionId");

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
                        .HasForeignKey("InstitucionId");

                    b.HasOne("PsychologyTest.Models.PsyTestUser", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("PsychologyTest.Models.Grupo", b =>
                {
                    b.HasOne("PsychologyTest.Models.Institucion", "Institucion")
                        .WithMany("Grupos")
                        .HasForeignKey("InstitucionId");
                });

            modelBuilder.Entity("PsychologyTest.Models.Psicologo", b =>
                {
                    b.HasOne("PsychologyTest.Models.Grupo", "Grupo")
                        .WithMany()
                        .HasForeignKey("GrupoId");

                    b.HasOne("PsychologyTest.Models.Institucion", "Institucion")
                        .WithMany()
                        .HasForeignKey("InstitucionId");

                    b.HasOne("PsychologyTest.Models.PsyTestUser", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });
        }
    }
}
