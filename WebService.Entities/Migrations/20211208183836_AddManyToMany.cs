using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebService.Entities.Migrations
{
    public partial class AddManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Materials_MaterialId",
                table: "Medias");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgrammingLanguages_Materials_MaterialId",
                table: "ProgrammingLanguages");

            migrationBuilder.DropIndex(
                name: "IX_ProgrammingLanguages_MaterialId",
                table: "ProgrammingLanguages");

            migrationBuilder.DropIndex(
                name: "IX_Medias_MaterialId",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "ProgrammingLanguages");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "Medias");

            migrationBuilder.CreateTable(
                name: "MaterialMedia",
                columns: table => new
                {
                    MaterialsId = table.Column<int>(type: "integer", nullable: false),
                    MediasId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialMedia", x => new { x.MaterialsId, x.MediasId });
                    table.ForeignKey(
                        name: "FK_MaterialMedia_Materials_MaterialsId",
                        column: x => x.MaterialsId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialMedia_Medias_MediasId",
                        column: x => x.MediasId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialProgrammingLanguage",
                columns: table => new
                {
                    MaterialsId = table.Column<int>(type: "integer", nullable: false),
                    ProgrammingLanguagesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialProgrammingLanguage", x => new { x.MaterialsId, x.ProgrammingLanguagesId });
                    table.ForeignKey(
                        name: "FK_MaterialProgrammingLanguage_Materials_MaterialsId",
                        column: x => x.MaterialsId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialProgrammingLanguage_ProgrammingLanguages_Programmin~",
                        column: x => x.ProgrammingLanguagesId,
                        principalTable: "ProgrammingLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialMedia_MediasId",
                table: "MaterialMedia",
                column: "MediasId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialProgrammingLanguage_ProgrammingLanguagesId",
                table: "MaterialProgrammingLanguage",
                column: "ProgrammingLanguagesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialMedia");

            migrationBuilder.DropTable(
                name: "MaterialProgrammingLanguage");

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "ProgrammingLanguages",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "Medias",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammingLanguages_MaterialId",
                table: "ProgrammingLanguages",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_MaterialId",
                table: "Medias",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Materials_MaterialId",
                table: "Medias",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgrammingLanguages_Materials_MaterialId",
                table: "ProgrammingLanguages",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id");
        }
    }
}
