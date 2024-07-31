using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi7.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoNumeroVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumeroVilla",
                columns: table => new
                {
                    NoVilla = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeroVilla", x => x.NoVilla);
                    table.ForeignKey(
                        name: "FK_NumeroVilla_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_NumeroVilla_VillaId",
                table: "NumeroVilla",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumeroVilla");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 7, 25, 2, 8, 26, 762, DateTimeKind.Local).AddTicks(30), new DateTime(2024, 7, 25, 2, 8, 26, 762, DateTimeKind.Local).AddTicks(45) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 7, 25, 2, 8, 26, 762, DateTimeKind.Local).AddTicks(49), new DateTime(2024, 7, 25, 2, 8, 26, 762, DateTimeKind.Local).AddTicks(50) });
        }
    }
}
