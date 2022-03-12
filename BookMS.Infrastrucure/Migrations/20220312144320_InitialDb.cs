using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMS.Infrastrucure.Migrations;
public partial class InitialDb : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "BookImages",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ImageSize = table.Column<long>(type: "bigint", nullable: false),
                CretetionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BookImages", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "BookTypes",
            columns: table => new
            {
                Id = table.Column<byte>(type: "tinyint", nullable: false),
                TypeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                CretetionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BookTypes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Categories",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Titel = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                ParentId = table.Column<int>(type: "int", nullable: false),
                CretetionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Categories", x => x.Id);
                table.ForeignKey(
                    name: "FK_Categories_Categories_ParentId",
                    column: x => x.ParentId,
                    principalTable: "Categories",
                    principalColumn: "Id",
                    onUpdate:ReferentialAction.NoAction,
                    onDelete: ReferentialAction.NoAction);
            });

        migrationBuilder.CreateTable(
            name: "Books",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                BookTitel = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                BookDescription = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                CoverImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                IsAvaible = table.Column<bool>(type: "bit", nullable: false),
                PageSize = table.Column<int>(type: "int", nullable: false),
                CategoryId = table.Column<int>(type: "int", nullable: false),
                CretetionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Books", x => x.Id);
                table.ForeignKey(
                    name: "FK_Books_Categories_CategoryId",
                    column: x => x.CategoryId,
                    principalTable: "Categories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "BookBookImages",
            columns: table => new
            {
                BooksId = table.Column<long>(type: "bigint", nullable: false),
                ImagesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BookBookImages", x => new { x.BooksId, x.ImagesId });
                table.ForeignKey(
                    name: "FK_BookBookImages_BookImages_ImagesId",
                    column: x => x.ImagesId,
                    principalTable: "BookImages",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_BookBookImages_Books_BooksId",
                    column: x => x.BooksId,
                    principalTable: "Books",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "BookBookType",
            columns: table => new
            {
                BookTypesId = table.Column<byte>(type: "tinyint", nullable: false),
                BooksId = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BookBookType", x => new { x.BookTypesId, x.BooksId });
                table.ForeignKey(
                    name: "FK_BookBookType_Books_BooksId",
                    column: x => x.BooksId,
                    principalTable: "Books",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_BookBookType_BookTypes_BookTypesId",
                    column: x => x.BookTypesId,
                    principalTable: "BookTypes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "BookFeatures",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Titel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Value = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                BookId = table.Column<long>(type: "bigint", nullable: false),
                CretetionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BookFeatures", x => x.Id);
                table.ForeignKey(
                    name: "FK_BookFeatures_Books_BookId",
                    column: x => x.BookId,
                    principalTable: "Books",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_BookBookImages_ImagesId",
            table: "BookBookImages",
            column: "ImagesId");

        migrationBuilder.CreateIndex(
            name: "IX_BookBookType_BooksId",
            table: "BookBookType",
            column: "BooksId");

        migrationBuilder.CreateIndex(
            name: "IX_BookFeatures_BookId",
            table: "BookFeatures",
            column: "BookId");

        migrationBuilder.CreateIndex(
            name: "IX_Books_CategoryId",
            table: "Books",
            column: "CategoryId");

        migrationBuilder.CreateIndex(
            name: "IX_Categories_ParentId",
            table: "Categories",
            column: "ParentId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "BookBookImages");

        migrationBuilder.DropTable(
            name: "BookBookType");

        migrationBuilder.DropTable(
            name: "BookFeatures");

        migrationBuilder.DropTable(
            name: "BookImages");

        migrationBuilder.DropTable(
            name: "BookTypes");

        migrationBuilder.DropTable(
            name: "Books");

        migrationBuilder.DropTable(
            name: "Categories");
    }
}
