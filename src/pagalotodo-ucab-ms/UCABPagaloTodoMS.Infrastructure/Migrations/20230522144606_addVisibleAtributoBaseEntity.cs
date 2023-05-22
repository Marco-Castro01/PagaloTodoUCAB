using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UCABPagaloTodoMS.Infrastructure.Migrations
{
    public partial class addVisibleAtributoBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pago_Consumidor_consumidorId",
                table: "Pago");

            migrationBuilder.DropForeignKey(
                name: "FK_Pago_Servicio_servicioId",
                table: "Pago");

            migrationBuilder.AddColumn<bool>(
                name: "visible",
                table: "Valores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "visible",
                table: "Servicio",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "visible",
                table: "PrestadorServicio",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "servicioId",
                table: "Pago",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "consumidorId",
                table: "Pago",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "visible",
                table: "Pago",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "visible",
                table: "Consumidor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "visible",
                table: "CamposConciliacion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "visible",
                table: "Admin",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Pago_Consumidor_consumidorId",
                table: "Pago",
                column: "consumidorId",
                principalTable: "Consumidor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pago_Servicio_servicioId",
                table: "Pago",
                column: "servicioId",
                principalTable: "Servicio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pago_Consumidor_consumidorId",
                table: "Pago");

            migrationBuilder.DropForeignKey(
                name: "FK_Pago_Servicio_servicioId",
                table: "Pago");

            migrationBuilder.DropColumn(
                name: "visible",
                table: "Valores");

            migrationBuilder.DropColumn(
                name: "visible",
                table: "Servicio");

            migrationBuilder.DropColumn(
                name: "visible",
                table: "PrestadorServicio");

            migrationBuilder.DropColumn(
                name: "visible",
                table: "Pago");

            migrationBuilder.DropColumn(
                name: "visible",
                table: "Consumidor");

            migrationBuilder.DropColumn(
                name: "visible",
                table: "CamposConciliacion");

            migrationBuilder.DropColumn(
                name: "visible",
                table: "Admin");

            migrationBuilder.AlterColumn<Guid>(
                name: "servicioId",
                table: "Pago",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "consumidorId",
                table: "Pago",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Pago_Consumidor_consumidorId",
                table: "Pago",
                column: "consumidorId",
                principalTable: "Consumidor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pago_Servicio_servicioId",
                table: "Pago",
                column: "servicioId",
                principalTable: "Servicio",
                principalColumn: "Id");
        }
    }
}
