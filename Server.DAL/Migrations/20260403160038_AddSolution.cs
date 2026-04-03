using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Server.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSolution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentSolutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    SolutionChatId = table.Column<int>(type: "integer", nullable: true),
                    SolutionText = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentSolutions_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentSolutions_TaskEducation_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskEducation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolutionChats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SolutionId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolutionChats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolutionChats_StudentSolutions_SolutionId",
                        column: x => x.SolutionId,
                        principalTable: "StudentSolutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolutionFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SolutionId = table.Column<int>(type: "integer", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    OriginalFileName = table.Column<string>(type: "text", nullable: false),
                    PhysicalPath = table.Column<string>(type: "text", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    FileType = table.Column<string>(type: "text", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolutionFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolutionFiles_StudentSolutions_SolutionId",
                        column: x => x.SolutionId,
                        principalTable: "StudentSolutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessagesInChat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChatId = table.Column<int>(type: "integer", nullable: false),
                    SenderId = table.Column<int>(type: "integer", nullable: false),
                    SenderRole = table.Column<byte>(type: "smallint", nullable: false),
                    MessageText = table.Column<string>(type: "text", nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessagesInChat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessagesInChat_SolutionChats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "SolutionChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilesInChat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MessageId = table.Column<int>(type: "integer", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    PhysicalPath = table.Column<string>(type: "text", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    FileType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesInChat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilesInChat_MessagesInChat_MessageId",
                        column: x => x.MessageId,
                        principalTable: "MessagesInChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilesInChat_MessageId",
                table: "FilesInChat",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessagesInChat_ChatId",
                table: "MessagesInChat",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_SolutionChats_SolutionId",
                table: "SolutionChats",
                column: "SolutionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolutionFiles_SolutionId",
                table: "SolutionFiles",
                column: "SolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSolutions_StudentId",
                table: "StudentSolutions",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSolutions_TaskId",
                table: "StudentSolutions",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilesInChat");

            migrationBuilder.DropTable(
                name: "SolutionFiles");

            migrationBuilder.DropTable(
                name: "MessagesInChat");

            migrationBuilder.DropTable(
                name: "SolutionChats");

            migrationBuilder.DropTable(
                name: "StudentSolutions");
        }
    }
}
