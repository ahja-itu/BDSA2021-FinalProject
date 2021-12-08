using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebService.Entities.Migrations
{
    public partial class bob2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Materials_MaterialId",
                table: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_Levels_MaterialId",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "Levels");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "Rating",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "Materials",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "LevelMaterial",
                columns: table => new
                {
                    LevelsId = table.Column<int>(type: "integer", nullable: false),
                    MaterialsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelMaterial", x => new { x.LevelsId, x.MaterialsId });
                    table.ForeignKey(
                        name: "FK_LevelMaterial_Levels_LevelsId",
                        column: x => x.LevelsId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LevelMaterial_Materials_MaterialsId",
                        column: x => x.MaterialsId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LevelMaterial_MaterialsId",
                table: "LevelMaterial",
                column: "MaterialsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LevelMaterial");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "Rating",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "Materials",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "Levels",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Levels_MaterialId",
                table: "Levels",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_Materials_MaterialId",
                table: "Levels",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id");
        }
    }
}
