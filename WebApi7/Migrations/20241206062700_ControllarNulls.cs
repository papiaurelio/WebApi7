using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi7.Migrations
{
    /// <inheritdoc />
    public partial class ControllarNulls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 12, 6, 0, 27, 0, 449, DateTimeKind.Local).AddTicks(3077), new DateTime(2024, 12, 6, 0, 27, 0, 449, DateTimeKind.Local).AddTicks(3094) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 12, 6, 0, 27, 0, 449, DateTimeKind.Local).AddTicks(3096), new DateTime(2024, 12, 6, 0, 27, 0, 449, DateTimeKind.Local).AddTicks(3097) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 7, 31, 8, 18, 45, 263, DateTimeKind.Local).AddTicks(1551), new DateTime(2024, 7, 31, 8, 18, 45, 263, DateTimeKind.Local).AddTicks(1566) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 7, 31, 8, 18, 45, 263, DateTimeKind.Local).AddTicks(1569), new DateTime(2024, 7, 31, 8, 18, 45, 263, DateTimeKind.Local).AddTicks(1570) });
        }
    }
}
