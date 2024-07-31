﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi7.Datos;

#nullable disable

namespace WebApi7.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240731141845_AgregandoNumeroVilla")]
    partial class AgregandoNumeroVilla
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApi7.Models.NumeroVilla", b =>
                {
                    b.Property<int>("NoVilla")
                        .HasColumnType("int");

                    b.Property<string>("Detalle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("VillaId")
                        .HasColumnType("int");

                    b.HasKey("NoVilla");

                    b.HasIndex("VillaId");

                    b.ToTable("NumeroVilla");
                });

            modelBuilder.Entity("WebApi7.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenidad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detalle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagenUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("MetrosCuadrados")
                        .HasColumnType("float");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ocupantes")
                        .HasColumnType("int");

                    b.Property<int>("Tarifa")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenidad = "",
                            Detalle = "Casa donde se vive Byron",
                            FechaActualizacion = new DateTime(2024, 7, 31, 8, 18, 45, 263, DateTimeKind.Local).AddTicks(1551),
                            FechaCreacion = new DateTime(2024, 7, 31, 8, 18, 45, 263, DateTimeKind.Local).AddTicks(1566),
                            ImagenUrl = "",
                            MetrosCuadrados = 400.0,
                            Nombre = "Casa Byron",
                            Ocupantes = 4,
                            Tarifa = 5
                        },
                        new
                        {
                            Id = 2,
                            Amenidad = "ABC",
                            Detalle = "Joselandia",
                            FechaActualizacion = new DateTime(2024, 7, 31, 8, 18, 45, 263, DateTimeKind.Local).AddTicks(1569),
                            FechaCreacion = new DateTime(2024, 7, 31, 8, 18, 45, 263, DateTimeKind.Local).AddTicks(1570),
                            ImagenUrl = "",
                            MetrosCuadrados = 800.0,
                            Nombre = "Casa Jose",
                            Ocupantes = 10,
                            Tarifa = 44
                        });
                });

            modelBuilder.Entity("WebApi7.Models.NumeroVilla", b =>
                {
                    b.HasOne("WebApi7.Models.Villa", "Villa")
                        .WithMany()
                        .HasForeignKey("VillaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Villa");
                });
#pragma warning restore 612, 618
        }
    }
}
