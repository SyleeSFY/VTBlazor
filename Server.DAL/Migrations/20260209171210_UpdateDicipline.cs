using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Server.DLL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDicipline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Course",
                table: "Disciplines",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isMagistracy",
                table: "Disciplines",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TrainedGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    isAS = table.Column<bool>(type: "boolean", nullable: false),
                    isPO = table.Column<bool>(type: "boolean", nullable: false),
                    isVM = table.Column<bool>(type: "boolean", nullable: false),
                    DisciplineId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainedGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainedGroups_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_Course",
                table: "Disciplines",
                column: "Course");

            migrationBuilder.CreateIndex(
                name: "IX_TrainedGroups_DisciplineId",
                table: "TrainedGroups",
                column: "DisciplineId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainedGroups");

            migrationBuilder.DropIndex(
                name: "IX_Disciplines_Course",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "Course",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "isMagistracy",
                table: "Disciplines");
        }
    }
}
