using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Hash = table.Column<string>(type: "varchar(100)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("71fc7674-18c7-4a01-ad55-fbecdfd7feda"), "Admin" },
                    { new Guid("89432022-e55f-48fb-92d9-29ccd24d7eca"), "User" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Hash", "Name", "RoleId" },
                values: new object[,]
                {
                    { new Guid("1d700ebe-fcd2-4439-9470-2ff1ee635b7d"), "HqWx1NypREjA4NyDYrrDvw==.2aFPO9MsQ2E6FshrYQNB/aXYfFoGNJNoM9b3V080vrA=", "user", new Guid("89432022-e55f-48fb-92d9-29ccd24d7eca") },
                    { new Guid("799b8043-067a-4c67-8175-e28d431e8e8d"), "PLDHFD75aPuvzK2XFZPXpw==.pU451GhsQ0RRi0n5AgDGkJCOXv0o+XeZp0rTlxDsulA=", "admin", new Guid("71fc7674-18c7-4a01-ad55-fbecdfd7feda") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
