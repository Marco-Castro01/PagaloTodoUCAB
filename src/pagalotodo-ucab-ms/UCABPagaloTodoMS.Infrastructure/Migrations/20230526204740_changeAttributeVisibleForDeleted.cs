using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UCABPagaloTodoMS.Infrastructure.Migrations
{
    public partial class changeAttributeVisibleForDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "visible",
                table: "Valores",
                newName: "deleted");

            migrationBuilder.RenameColumn(
                name: "visible",
                table: "Usuarios",
                newName: "deleted");

            migrationBuilder.RenameColumn(
                name: "visible",
                table: "Servicio",
                newName: "deleted");

            migrationBuilder.RenameColumn(
                name: "visible",
                table: "Pago",
                newName: "deleted");

            migrationBuilder.RenameColumn(
                name: "visible",
                table: "CamposConciliacion",
                newName: "deleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "deleted",
                table: "Valores",
                newName: "visible");

            migrationBuilder.RenameColumn(
                name: "deleted",
                table: "Usuarios",
                newName: "visible");

            migrationBuilder.RenameColumn(
                name: "deleted",
                table: "Servicio",
                newName: "visible");

            migrationBuilder.RenameColumn(
                name: "deleted",
                table: "Pago",
                newName: "visible");

            migrationBuilder.RenameColumn(
                name: "deleted",
                table: "CamposConciliacion",
                newName: "visible");
        }
    }
}
