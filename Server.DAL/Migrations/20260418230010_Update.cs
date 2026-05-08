using System;
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
                name: "Disciplines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameDiscipline = table.Column<string>(type: "text", nullable: false),
                    Course = table.Column<int>(type: "integer", nullable: false),
                    isMagistracy = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Role = table.Column<byte>(type: "smallint", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Position = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Educators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Profession = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    AcademicDegree = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Educators_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    StudentCard = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Image = table.Column<byte[]>(type: "bytea", nullable: false)
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
                name: "TaskEducation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiciplineId = table.Column<int>(type: "integer", nullable: false),
                    EducatorId = table.Column<int>(type: "integer", nullable: false),
                    TaskName = table.Column<string>(type: "text", nullable: false),
                    TaskDescription = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEducation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskEducation_Disciplines_DiciplineId",
                        column: x => x.DiciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskEducation_Educators_EducatorId",
                        column: x => x.EducatorId,
                        principalTable: "Educators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "StudentSolutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
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
                name: "TaskFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    PhysicalPath = table.Column<string>(type: "text", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FileType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskFile_TaskEducation_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskEducation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskGroups",
                columns: table => new
                {
                    GroupsId = table.Column<int>(type: "integer", nullable: false),
                    TaskEducationsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskGroups", x => new { x.GroupsId, x.TaskEducationsId });
                    table.ForeignKey(
                        name: "FK_TaskGroups_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskGroups_TaskEducation_TaskEducationsId",
                        column: x => x.TaskEducationsId,
                        principalTable: "TaskEducation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Admins_UserId",
                table: "Admins",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_Course",
                table: "Disciplines",
                column: "Course");

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

            migrationBuilder.CreateIndex(
                name: "IX_Educators_UserId",
                table: "Educators",
                column: "UserId",
                unique: true);

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
                name: "IX_Students_GroupId",
                table: "Students",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentCard",
                table: "Students",
                column: "StudentCard",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentSolutions_StudentId",
                table: "StudentSolutions",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSolutions_TaskId",
                table: "StudentSolutions",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEducation_DiciplineId",
                table: "TaskEducation",
                column: "DiciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEducation_EducatorId",
                table: "TaskEducation",
                column: "EducatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskFile_TaskId",
                table: "TaskFile",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskGroups_TaskEducationsId",
                table: "TaskGroups",
                column: "TaskEducationsId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainedGroups_DisciplineId",
                table: "TrainedGroups",
                column: "DisciplineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "EducatorDisciplines");

            migrationBuilder.DropTable(
                name: "FilesInChat");

            migrationBuilder.DropTable(
                name: "SolutionFiles");

            migrationBuilder.DropTable(
                name: "TaskFile");

            migrationBuilder.DropTable(
                name: "TaskGroups");

            migrationBuilder.DropTable(
                name: "TrainedGroups");

            migrationBuilder.DropTable(
                name: "EducatorAdditionalInfos");

            migrationBuilder.DropTable(
                name: "MessagesInChat");

            migrationBuilder.DropTable(
                name: "SolutionChats");

            migrationBuilder.DropTable(
                name: "StudentSolutions");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "TaskEducation");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "Educators");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
