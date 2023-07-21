using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UCABPagaloTodoMS.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class AddTablaUrlsFirebase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArchivoFirebase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    urlFirebase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prestadorServicioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivoFirebase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchivoFirebase_Usuarios_prestadorServicioId",
                        column: x => x.prestadorServicioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchivoFirebase_prestadorServicioId",
                table: "ArchivoFirebase",
                column: "prestadorServicioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivoFirebase");
        }
    }
}
