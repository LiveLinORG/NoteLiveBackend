﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;

#nullable disable

namespace NoteLiveBackend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240607041107_migracion")]
    partial class migracion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("NoteLiveBackend.Users.Domain.Model.Aggregates.Alumno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("correo");

                    b.Property<string>("LastNames")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("last_names");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.ToTable("p_k_alumno", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
