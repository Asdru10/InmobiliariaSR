using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoInmobilaria.Migrations
{
    /// <inheritdoc />
    public partial class CambioRelacionUbicacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ubicaciones_PropiedadId",
                table: "Ubicaciones");

            migrationBuilder.CreateIndex(
                name: "IX_Ubicaciones_PropiedadId",
                table: "Ubicaciones",
                column: "PropiedadId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ubicaciones_PropiedadId",
                table: "Ubicaciones");

            migrationBuilder.CreateIndex(
                name: "IX_Ubicaciones_PropiedadId",
                table: "Ubicaciones",
                column: "PropiedadId");
        }
    }
}
