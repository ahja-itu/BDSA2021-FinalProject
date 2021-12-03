using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebService.Entities.Migrations
{
    public partial class UpdatedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Materials_MaterialId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Languages_LanguageId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Materials_MaterialId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Materials_MaterialId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_MaterialId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_MaterialId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_Value",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_FirstName_SurName",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_MaterialId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Tags");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "Rating");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "Author");

            migrationBuilder.RenameColumn(
                name: "EducationLevel",
                table: "Levels",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Levels_EducationLevel",
                table: "Levels",
                newName: "IX_Levels_Name");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Materials",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "Materials",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Materials",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Materials",
                type: "character varying(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Materials",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "MaterialId",
                table: "Rating",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Rating",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "Reviewer",
                table: "Rating",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "Rating",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "MaterialId",
                table: "Author",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Author",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rating",
                table: "Rating",
                columns: new[] { "MaterialId", "Reviewer" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Author",
                table: "Author",
                columns: new[] { "MaterialId", "FirstName", "SurName" });

            migrationBuilder.CreateTable(
                name: "WeightedTag",
                columns: table => new
                {
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MaterialId = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightedTag", x => new { x.MaterialId, x.Name });
                    table.ForeignKey(
                        name: "FK_WeightedTag_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Author_Materials_MaterialId",
                table: "Author",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Languages_LanguageId",
                table: "Materials",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Materials_MaterialId",
                table: "Rating",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Author_Materials_MaterialId",
                table: "Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Languages_LanguageId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Materials_MaterialId",
                table: "Rating");

            migrationBuilder.DropTable(
                name: "WeightedTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rating",
                table: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Author",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "URL",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "Reviewer",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Rating");

            migrationBuilder.RenameTable(
                name: "Rating",
                newName: "Ratings");

            migrationBuilder.RenameTable(
                name: "Author",
                newName: "Authors");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Levels",
                newName: "EducationLevel");

            migrationBuilder.RenameIndex(
                name: "IX_Levels_Name",
                table: "Levels",
                newName: "IX_Levels_EducationLevel");

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "Tags",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Tags",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Materials",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "Materials",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Ratings",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "MaterialId",
                table: "Ratings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Authors",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "MaterialId",
                table: "Authors",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_MaterialId",
                table: "Tags",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_MaterialId",
                table: "Ratings",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_Value",
                table: "Ratings",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_FirstName_SurName",
                table: "Authors",
                columns: new[] { "FirstName", "SurName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_MaterialId",
                table: "Authors",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Materials_MaterialId",
                table: "Authors",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Languages_LanguageId",
                table: "Materials",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Materials_MaterialId",
                table: "Ratings",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Materials_MaterialId",
                table: "Tags",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id");
        }
    }
}
