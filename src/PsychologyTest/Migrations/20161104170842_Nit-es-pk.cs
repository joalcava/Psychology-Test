using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PsychologyTest.Migrations
{
    public partial class Nitespk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Instituciones_InstitucionId",
                table: "Estudiantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Grupos_Instituciones_InstitucionId",
                table: "Grupos");

            migrationBuilder.DropForeignKey(
                name: "FK_Psicologos_Grupos_GrupoId",
                table: "Psicologos");

            migrationBuilder.DropForeignKey(
                name: "FK_Psicologos_Instituciones_InstitucionId",
                table: "Psicologos");

            migrationBuilder.DropIndex(
                name: "IX_Psicologos_GrupoId",
                table: "Psicologos");

            migrationBuilder.DropIndex(
                name: "IX_Psicologos_InstitucionId",
                table: "Psicologos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instituciones",
                table: "Instituciones");

            migrationBuilder.DropIndex(
                name: "IX_Grupos_InstitucionId",
                table: "Grupos");

            migrationBuilder.DropIndex(
                name: "IX_Estudiantes_InstitucionId",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "GrupoId",
                table: "Psicologos");

            migrationBuilder.DropColumn(
                name: "InstitucionId",
                table: "Psicologos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Instituciones");

            migrationBuilder.DropColumn(
                name: "InstitucionId",
                table: "Grupos");

            migrationBuilder.DropColumn(
                name: "InstitucionId",
                table: "Estudiantes");

            migrationBuilder.CreateTable(
                name: "PruebasAsignadas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Completado = table.Column<bool>(nullable: false),
                    Diagnostico = table.Column<string>(nullable: true),
                    EncuestadoId = table.Column<int>(nullable: true),
                    FechaAsignacion = table.Column<DateTime>(nullable: false),
                    FechaFinalizacion = table.Column<DateTime>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    Iniciado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PruebasAsignadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PruebasAsignadas_Estudiantes_EncuestadoId",
                        column: x => x.EncuestadoId,
                        principalTable: "Estudiantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PruebasPsicologicas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Activo = table.Column<bool>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaCreado = table.Column<DateTime>(nullable: false),
                    FechaModificado = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PruebasPsicologicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposDePregunta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    OpcionMultiple = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDePregunta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Preguntas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaModificado = table.Column<DateTime>(nullable: false),
                    Orden = table.Column<int>(nullable: false),
                    PruebaId = table.Column<int>(nullable: true),
                    RespuestaEsperada = table.Column<string>(nullable: true),
                    Texto = table.Column<string>(nullable: true),
                    TipoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preguntas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Preguntas_PruebasPsicologicas_PruebaId",
                        column: x => x.PruebaId,
                        principalTable: "PruebasPsicologicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Preguntas_TiposDePregunta_TipoId",
                        column: x => x.TipoId,
                        principalTable: "TiposDePregunta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpcionesMultiples",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PreguntaOrigenId = table.Column<int>(nullable: true),
                    Texto = table.Column<string>(nullable: true),
                    Valor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcionesMultiples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpcionesMultiples_Preguntas_PreguntaOrigenId",
                        column: x => x.PreguntaOrigenId,
                        principalTable: "Preguntas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddColumn<int>(
                name: "InstitucionNit",
                table: "Grupos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PsicologoId",
                table: "Grupos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstitucionNit",
                table: "Estudiantes",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Nit",
                table: "Instituciones",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instituciones",
                table: "Instituciones",
                column: "Nit");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_InstitucionNit",
                table: "Grupos",
                column: "InstitucionNit");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_PsicologoId",
                table: "Grupos",
                column: "PsicologoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_InstitucionNit",
                table: "Estudiantes",
                column: "InstitucionNit");

            migrationBuilder.CreateIndex(
                name: "IX_OpcionesMultiples_PreguntaOrigenId",
                table: "OpcionesMultiples",
                column: "PreguntaOrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_Preguntas_PruebaId",
                table: "Preguntas",
                column: "PruebaId");

            migrationBuilder.CreateIndex(
                name: "IX_Preguntas_TipoId",
                table: "Preguntas",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_PruebasAsignadas_EncuestadoId",
                table: "PruebasAsignadas",
                column: "EncuestadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Instituciones_InstitucionNit",
                table: "Estudiantes",
                column: "InstitucionNit",
                principalTable: "Instituciones",
                principalColumn: "Nit",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grupos_Instituciones_InstitucionNit",
                table: "Grupos",
                column: "InstitucionNit",
                principalTable: "Instituciones",
                principalColumn: "Nit",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grupos_Psicologos_PsicologoId",
                table: "Grupos",
                column: "PsicologoId",
                principalTable: "Psicologos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Instituciones_InstitucionNit",
                table: "Estudiantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Grupos_Instituciones_InstitucionNit",
                table: "Grupos");

            migrationBuilder.DropForeignKey(
                name: "FK_Grupos_Psicologos_PsicologoId",
                table: "Grupos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instituciones",
                table: "Instituciones");

            migrationBuilder.DropIndex(
                name: "IX_Grupos_InstitucionNit",
                table: "Grupos");

            migrationBuilder.DropIndex(
                name: "IX_Grupos_PsicologoId",
                table: "Grupos");

            migrationBuilder.DropIndex(
                name: "IX_Estudiantes_InstitucionNit",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "InstitucionNit",
                table: "Grupos");

            migrationBuilder.DropColumn(
                name: "PsicologoId",
                table: "Grupos");

            migrationBuilder.DropColumn(
                name: "InstitucionNit",
                table: "Estudiantes");

            migrationBuilder.DropTable(
                name: "OpcionesMultiples");

            migrationBuilder.DropTable(
                name: "PruebasAsignadas");

            migrationBuilder.DropTable(
                name: "Preguntas");

            migrationBuilder.DropTable(
                name: "PruebasPsicologicas");

            migrationBuilder.DropTable(
                name: "TiposDePregunta");

            migrationBuilder.AddColumn<int>(
                name: "GrupoId",
                table: "Psicologos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstitucionId",
                table: "Psicologos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Instituciones",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "InstitucionId",
                table: "Grupos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstitucionId",
                table: "Estudiantes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Psicologos_GrupoId",
                table: "Psicologos",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Psicologos_InstitucionId",
                table: "Psicologos",
                column: "InstitucionId");

            migrationBuilder.AlterColumn<string>(
                name: "Nit",
                table: "Instituciones",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instituciones",
                table: "Instituciones",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_InstitucionId",
                table: "Grupos",
                column: "InstitucionId");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_InstitucionId",
                table: "Estudiantes",
                column: "InstitucionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Instituciones_InstitucionId",
                table: "Estudiantes",
                column: "InstitucionId",
                principalTable: "Instituciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grupos_Instituciones_InstitucionId",
                table: "Grupos",
                column: "InstitucionId",
                principalTable: "Instituciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Psicologos_Grupos_GrupoId",
                table: "Psicologos",
                column: "GrupoId",
                principalTable: "Grupos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Psicologos_Instituciones_InstitucionId",
                table: "Psicologos",
                column: "InstitucionId",
                principalTable: "Instituciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
