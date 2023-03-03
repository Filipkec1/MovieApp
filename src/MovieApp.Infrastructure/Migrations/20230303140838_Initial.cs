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
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                });

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
                name: "CategoryMovie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MovieId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryMovie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryMovie_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryMovie_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("19f977cc-3916-4a1f-908c-f48700a40880"), "Action" });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("02d3fa37-f439-4e67-a87a-1dcf1d077ad6"), "Blade Runner 2049" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("71fc7674-18c7-4a01-ad55-fbecdfd7feda"), "Admin" },
                    { new Guid("89432022-e55f-48fb-92d9-29ccd24d7eca"), "User" }
                });

            migrationBuilder.InsertData(
                table: "CategoryMovie",
                columns: new[] { "Id", "CategoryId", "MovieId" },
                values: new object[] { new Guid("74600e89-170c-41b7-8aae-48f8ec08630d"), new Guid("19f977cc-3916-4a1f-908c-f48700a40880"), new Guid("02d3fa37-f439-4e67-a87a-1dcf1d077ad6") });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Hash", "Name", "RoleId" },
                values: new object[,]
                {
                    { new Guid("1d700ebe-fcd2-4439-9470-2ff1ee635b7d"), "HqWx1NypREjA4NyDYrrDvw==.2aFPO9MsQ2E6FshrYQNB/aXYfFoGNJNoM9b3V080vrA=", "user", new Guid("89432022-e55f-48fb-92d9-29ccd24d7eca") },
                    { new Guid("799b8043-067a-4c67-8175-e28d431e8e8d"), "PLDHFD75aPuvzK2XFZPXpw==.pU451GhsQ0RRi0n5AgDGkJCOXv0o+XeZp0rTlxDsulA=", "admin", new Guid("71fc7674-18c7-4a01-ad55-fbecdfd7feda") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryMovie_CategoryId",
                table: "CategoryMovie",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryMovie_MovieId",
                table: "CategoryMovie",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Name",
                table: "User",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryMovie");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
