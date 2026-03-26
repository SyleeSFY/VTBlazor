using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupTaskEducation_Groups_GroupsId",
                table: "GroupTaskEducation");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTaskEducation_TaskEducation_TaskEducationsId",
                table: "GroupTaskEducation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupTaskEducation",
                table: "GroupTaskEducation");

            migrationBuilder.RenameTable(
                name: "GroupTaskEducation",
                newName: "TaskGroups");

            migrationBuilder.RenameIndex(
                name: "IX_GroupTaskEducation_TaskEducationsId",
                table: "TaskGroups",
                newName: "IX_TaskGroups_TaskEducationsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskGroups",
                table: "TaskGroups",
                columns: new[] { "GroupsId", "TaskEducationsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TaskGroups_Groups_GroupsId",
                table: "TaskGroups",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskGroups_TaskEducation_TaskEducationsId",
                table: "TaskGroups",
                column: "TaskEducationsId",
                principalTable: "TaskEducation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskGroups_Groups_GroupsId",
                table: "TaskGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskGroups_TaskEducation_TaskEducationsId",
                table: "TaskGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskGroups",
                table: "TaskGroups");

            migrationBuilder.RenameTable(
                name: "TaskGroups",
                newName: "GroupTaskEducation");

            migrationBuilder.RenameIndex(
                name: "IX_TaskGroups_TaskEducationsId",
                table: "GroupTaskEducation",
                newName: "IX_GroupTaskEducation_TaskEducationsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupTaskEducation",
                table: "GroupTaskEducation",
                columns: new[] { "GroupsId", "TaskEducationsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTaskEducation_Groups_GroupsId",
                table: "GroupTaskEducation",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTaskEducation_TaskEducation_TaskEducationsId",
                table: "GroupTaskEducation",
                column: "TaskEducationsId",
                principalTable: "TaskEducation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
