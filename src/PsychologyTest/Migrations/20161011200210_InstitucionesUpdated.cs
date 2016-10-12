using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PsychologyTest.Migrations
{
    public partial class InstitucionesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WebSite",
                table: "Instituciones");

            migrationBuilder.DropColumn(
                name: "NumeroEstudiantes",
                table: "Grupos");

            migrationBuilder.AddColumn<string>(
                name: "Nit",
                table: "Instituciones",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SitioWeb",
                table: "Instituciones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nit",
                table: "Instituciones");

            migrationBuilder.DropColumn(
                name: "SitioWeb",
                table: "Instituciones");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaNacimiento",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "WebSite",
                table: "Instituciones",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumeroEstudiantes",
                table: "Grupos",
                nullable: false,
                defaultValue: 0);
        }
    }
}
