using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SolutionChatId",
                table: "StudentSolutions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SolutionChatId",
                table: "StudentSolutions",
                type: "integer",
                nullable: true);
        }
    }
}
