﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tarea_Inicio_de_Sesión_y_Registro.Models;

#nullable disable

namespace Tarea_Inicio_de_Sesión_y_Registro.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Contrasena")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Fecha_Creacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Fecha_Modificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
