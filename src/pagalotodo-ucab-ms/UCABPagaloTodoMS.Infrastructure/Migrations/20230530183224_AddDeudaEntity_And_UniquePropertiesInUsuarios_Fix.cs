using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UCABPagaloTodoMS.Infrastructure.Migrations
{
    public partial class AddDeudaEntity_And_UniquePropertiesInUsuarios_Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deuda_Usuarios_ConsumidorEntityId",
                table: "Deuda");

            migrationBuilder.DropIndex(
                name: "IX_Deuda_ConsumidorEntityId",
                table: "Deuda");

            migrationBuilder.DropColumn(
                name: "ConsumidorEntityId",
                table: "Deuda");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ConsumidorEntityId",
                table: "Deuda",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deuda_ConsumidorEntityId",
                table: "Deuda",
                column: "ConsumidorEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deuda_Usuarios_ConsumidorEntityId",
                table: "Deuda",
                column: "ConsumidorEntityId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
