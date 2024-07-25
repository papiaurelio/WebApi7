using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi7.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoDatosDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Casa donde se vive Byron", new DateTime(2024, 7, 25, 2, 8, 26, 762, DateTimeKind.Local).AddTicks(30), new DateTime(2024, 7, 25, 2, 8, 26, 762, DateTimeKind.Local).AddTicks(45), "", 400.0, "Casa Byron", 4, 5 },
                    { 2, "ABC", "Joselandia", new DateTime(2024, 7, 25, 2, 8, 26, 762, DateTimeKind.Local).AddTicks(49), new DateTime(2024, 7, 25, 2, 8, 26, 762, DateTimeKind.Local).AddTicks(50), "", 800.0, "Casa Jose", 10, 44 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
