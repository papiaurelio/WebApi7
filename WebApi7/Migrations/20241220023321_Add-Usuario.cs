using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi7.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 12, 19, 20, 33, 21, 388, DateTimeKind.Local).AddTicks(3098), new DateTime(2024, 12, 19, 20, 33, 21, 388, DateTimeKind.Local).AddTicks(3111) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 12, 19, 20, 33, 21, 388, DateTimeKind.Local).AddTicks(3117), new DateTime(2024, 12, 19, 20, 33, 21, 388, DateTimeKind.Local).AddTicks(3118) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");

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
    }
}
