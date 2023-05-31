using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UCABPagaloTodoMS.Infrastructure.Migrations
{
    public partial class AddDeudaEntity_And_UniquePropertiesInUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "nickName",
                table: "Usuarios",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Usuarios",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "cedula",
                table: "Usuarios",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tipoServicio",
                table: "Servicio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Deuda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    identificador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    servicioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    deuda = table.Column<double>(type: "float", nullable: false),
                    ConsumidorEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deuda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deuda_Servicio_servicioId",
                        column: x => x.servicioId,
                        principalTable: "Servicio",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Deuda_Usuarios_ConsumidorEntityId",
                        column: x => x.ConsumidorEntityId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_cedula",
                table: "Usuarios",
                column: "cedula",
                unique: true,
                filter: "[cedula] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_name",
                table: "Usuarios",
                column: "name",
                unique: true,
                filter: "[name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_nickName",
                table: "Usuarios",
                column: "nickName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deuda_ConsumidorEntityId",
                table: "Deuda",
                column: "ConsumidorEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Deuda_servicioId",
                table: "Deuda",
                column: "servicioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deuda");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_cedula",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_name",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_nickName",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "tipoServicio",
                table: "Servicio");

            migrationBuilder.AlterColumn<string>(
                name: "nickName",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "cedula",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
