using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PsychologyTest.Models;

namespace PsychologyTest.Migrations
{
    [DbContext(typeof(PsyTestContext))]
    [Migration("20161008035248_MigracionInicial")]
    partial class MigracionInicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PsychologyTest.Models.Grupo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Grado");

                    b.Property<int?>("InstitucionId");

                    b.Property<string>("Jornada");

                    b.Property<string>("Nombre");

                    b.Property<int>("NumeroEstudiantes");

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

                    b.Property<string>("Nombre");

                    b.Property<string>("Telefono");

                    b.Property<string>("WebSite");

                    b.HasKey("Id");

                    b.ToTable("Instituciones");
                });

            modelBuilder.Entity("PsychologyTest.Models.Grupo", b =>
                {
                    b.HasOne("PsychologyTest.Models.Institucion")
                        .WithMany("Grupos")
                        .HasForeignKey("InstitucionId");
                });
        }
    }
}
