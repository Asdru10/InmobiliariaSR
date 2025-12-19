using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoInmobilaria.Migrations
{
    /// <inheritdoc />
    public partial class addCoordenadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Latitud",
                table: "Ubicaciones",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Longitud",
                table: "Ubicaciones",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitud",
                table: "Ubicaciones");

            migrationBuilder.DropColumn(
                name: "Longitud",
                table: "Ubicaciones");
        }
    }
}
