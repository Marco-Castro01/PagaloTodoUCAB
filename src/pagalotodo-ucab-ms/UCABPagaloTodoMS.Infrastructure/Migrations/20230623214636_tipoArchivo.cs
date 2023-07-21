using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace UCABPagaloTodoMS.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class tipoArchivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "tipoArchivo",
                table: "ArchivoFirebase",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tipoArchivo",
                table: "ArchivoFirebase");
        }
    }
}
