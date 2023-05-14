using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UCABPagaloTodoMS.Infrastructure.Migrations
{
    public partial class ArregloMigraciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_PrestadorServicio_PrestadorServicioEntityId",
                table: "Servicio");

            migrationBuilder.DropIndex(
                name: "IX_Servicio_PrestadorServicioEntityId",
                table: "Servicio");

            migrationBuilder.DropColumn(
                name: "PrestadorServicioEntityId",
                table: "Servicio");

            migrationBuilder.AddColumn<Guid>(
                name: "PrestadorServicioId",
                table: "Servicio",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CamposConciliacion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitud = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CamposConciliacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CamposConciliacionEntityServicioEntity",
                columns: table => new
                {
                    CamposConciliacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CamposConciliacionEntityServicioEntity", x => new { x.CamposConciliacionId, x.ServicioId });
                    table.ForeignKey(
                        name: "FK_CamposConciliacionEntityServicioEntity_CamposConciliacion_CamposConciliacionId",
                        column: x => x.CamposConciliacionId,
                        principalTable: "CamposConciliacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CamposConciliacionEntityServicioEntity_Servicio_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_PrestadorServicioId",
                table: "Servicio",
                column: "PrestadorServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_CamposConciliacionEntityServicioEntity_ServicioId",
                table: "CamposConciliacionEntityServicioEntity",
                column: "ServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_PrestadorServicio_PrestadorServicioId",
                table: "Servicio",
                column: "PrestadorServicioId",
                principalTable: "PrestadorServicio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_PrestadorServicio_PrestadorServicioId",
                table: "Servicio");

            migrationBuilder.DropTable(
                name: "CamposConciliacionEntityServicioEntity");

            migrationBuilder.DropTable(
                name: "CamposConciliacion");

            migrationBuilder.DropIndex(
                name: "IX_Servicio_PrestadorServicioId",
                table: "Servicio");

            migrationBuilder.DropColumn(
                name: "PrestadorServicioId",
                table: "Servicio");

            migrationBuilder.AddColumn<Guid>(
                name: "PrestadorServicioEntityId",
                table: "Servicio",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_PrestadorServicioEntityId",
                table: "Servicio",
                column: "PrestadorServicioEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_PrestadorServicio_PrestadorServicioEntityId",
                table: "Servicio",
                column: "PrestadorServicioEntityId",
                principalTable: "PrestadorServicio",
                principalColumn: "Id");
        }
    }
}
