using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetBoilerplate.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookStores",
                schema: "dotNetBoilerplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookStores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookStores_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "dotNetBoilerplate",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Books",
                schema: "dotNetBoilerplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Writer = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Genre = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    BookStoreId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_BookStores_BookStoreId",
                        column: x => x.BookStoreId,
                        principalSchema: "dotNetBoilerplate",
                        principalTable: "BookStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "dotNetBoilerplate",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                schema: "dotNetBoilerplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Books_BookId",
                        column: x => x.BookId,
                        principalSchema: "dotNetBoilerplate",
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "dotNetBoilerplate",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookStoreId",
                schema: "dotNetBoilerplate",
                table: "Books",
                column: "BookStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CreatedBy",
                schema: "dotNetBoilerplate",
                table: "Books",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BookStores_OwnerId",
                schema: "dotNetBoilerplate",
                table: "BookStores",
                column: "OwnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookId_CreatedBy",
                schema: "dotNetBoilerplate",
                table: "Reviews",
                columns: new[] { "BookId", "CreatedBy" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CreatedBy",
                schema: "dotNetBoilerplate",
                table: "Reviews",
                column: "CreatedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews",
                schema: "dotNetBoilerplate");

            migrationBuilder.DropTable(
                name: "Books",
                schema: "dotNetBoilerplate");

            migrationBuilder.DropTable(
                name: "BookStores",
                schema: "dotNetBoilerplate");
        }
    }
}
