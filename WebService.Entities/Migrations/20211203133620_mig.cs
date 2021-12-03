using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebService.Entities.Migrations
{
    public partial class mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "WeightedTags");

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SurName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MaterialId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => new { x.MaterialId, x.FirstName, x.SurName });
                    table.ForeignKey(
                        name: "FK_Author_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Reviewer = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MaterialId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => new { x.MaterialId, x.Reviewer });
                    table.ForeignKey(
                        name: "FK_Rating_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "WeightedTag");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SurName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => new { x.MaterialId, x.FirstName, x.SurName });
                    table.ForeignKey(
                        name: "FK_Authors_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "integer", nullable: false),
                    Reviewer = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => new { x.MaterialId, x.Reviewer });
                    table.ForeignKey(
                        name: "FK_Ratings_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeightedTags",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightedTags", x => new { x.MaterialId, x.Name });
                    table.ForeignKey(
                        name: "FK_WeightedTags_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
