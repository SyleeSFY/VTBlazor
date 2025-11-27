using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Server.DLL.Migrations
{
    /// <inheritdoc />
    public partial class CreateDiscipline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_educators",
                table: "educators");

            migrationBuilder.DropColumn(
                name: "image",
                table: "educators");

            migrationBuilder.RenameTable(
                name: "educators",
                newName: "Educators");

            migrationBuilder.RenameColumn(
                name: "fullname",
                table: "Educators",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "academicdegree",
                table: "Educators",
                newName: "AcademicDegree");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Educators",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Educators",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "AcademicDegree",
                table: "Educators",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "Educators",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Educators",
                table: "Educators",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameDiscipline = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducatorAdditionalInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EducatorId = table.Column<int>(type: "integer", nullable: false),
                    EducationLevel = table.Column<string>(type: "text", nullable: false),
                    AcademicTitle = table.Column<string>(type: "text", nullable: false),
                    SpecialtyOrFieldOfStudy = table.Column<string>(type: "text", nullable: false),
                    Qualification = table.Column<string>(type: "text", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<List<byte>>(type: "smallint[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducatorAdditionalInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducatorAdditionalInfos_Educators_EducatorId",
                        column: x => x.EducatorId,
                        principalTable: "Educators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducatorDisciplines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EducatorAdditionalInfoId = table.Column<int>(type: "integer", nullable: false),
                    DisciplineId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducatorDisciplines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducatorDisciplines_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducatorDisciplines_EducatorAdditionalInfos_EducatorAdditio~",
                        column: x => x.EducatorAdditionalInfoId,
                        principalTable: "EducatorAdditionalInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducatorAdditionalInfos_EducatorId",
                table: "EducatorAdditionalInfos",
                column: "EducatorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducatorDisciplines_DisciplineId",
                table: "EducatorDisciplines",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_EducatorDisciplines_EducatorAdditionalInfoId",
                table: "EducatorDisciplines",
                column: "EducatorAdditionalInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducatorDisciplines");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "EducatorAdditionalInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Educators",
                table: "Educators");

            migrationBuilder.DropColumn(
                name: "Profession",
                table: "Educators");

            migrationBuilder.RenameTable(
                name: "Educators",
                newName: "educators");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "educators",
                newName: "fullname");

            migrationBuilder.RenameColumn(
                name: "AcademicDegree",
                table: "educators",
                newName: "academicdegree");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "educators",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "fullname",
                table: "educators",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "academicdegree",
                table: "educators",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<byte[]>(
                name: "image",
                table: "educators",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_educators",
                table: "educators",
                column: "id");
        }
    }
}
