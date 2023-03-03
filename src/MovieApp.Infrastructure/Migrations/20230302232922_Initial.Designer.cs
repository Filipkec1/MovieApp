﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieApp.Infrastructure.Context;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MovieApp.Infrastructure.Migrations
{
    [DbContext(typeof(MovieAppContext))]
    [Migration("20230302232922_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MovieApp.Core.Models.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(5)");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = new Guid("71fc7674-18c7-4a01-ad55-fbecdfd7feda"),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("89432022-e55f-48fb-92d9-29ccd24d7eca"),
                            Name = "User"
                        });
                });

            modelBuilder.Entity("MovieApp.Core.Models.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("799b8043-067a-4c67-8175-e28d431e8e8d"),
                            Hash = "PLDHFD75aPuvzK2XFZPXpw==.pU451GhsQ0RRi0n5AgDGkJCOXv0o+XeZp0rTlxDsulA=",
                            Name = "admin",
                            RoleId = new Guid("71fc7674-18c7-4a01-ad55-fbecdfd7feda")
                        },
                        new
                        {
                            Id = new Guid("1d700ebe-fcd2-4439-9470-2ff1ee635b7d"),
                            Hash = "HqWx1NypREjA4NyDYrrDvw==.2aFPO9MsQ2E6FshrYQNB/aXYfFoGNJNoM9b3V080vrA=",
                            Name = "user",
                            RoleId = new Guid("89432022-e55f-48fb-92d9-29ccd24d7eca")
                        });
                });

            modelBuilder.Entity("MovieApp.Core.Models.Entities.User", b =>
                {
                    b.HasOne("MovieApp.Core.Models.Entities.Role", "Role")
                        .WithMany("User")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("MovieApp.Core.Models.Entities.Role", b =>
                {
                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}