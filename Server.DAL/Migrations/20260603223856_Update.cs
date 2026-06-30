using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Server.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatParticipants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SolutionChatId = table.Column<int>(type: "integer", nullable: false),
                    SenderId = table.Column<int>(type: "integer", nullable: false),
                    HasUnreadMessages = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatParticipants_SolutionChats_SolutionChatId",
                        column: x => x.SolutionChatId,
                        principalTable: "SolutionChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatParticipants_SenderId",
                table: "ChatParticipants",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatParticipants_SolutionChatId",
                table: "ChatParticipants",
                column: "SolutionChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatParticipants_SolutionChatId_SenderId",
                table: "ChatParticipants",
                columns: new[] { "SolutionChatId", "SenderId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatParticipants");
        }
    }
}
