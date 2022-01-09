using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoListApiSqlite.Migrations
{
    public partial class UserGroup_ManyToMany_Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_Groups_GroupsId",
                table: "GroupUser");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_Users_UsersId",
                table: "GroupUser");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "GroupUser",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "GroupsId",
                table: "GroupUser",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUser_UsersId",
                table: "GroupUser",
                newName: "IX_GroupUser_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_Groups_GroupId",
                table: "GroupUser",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_Users_UserId",
                table: "GroupUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_Groups_GroupId",
                table: "GroupUser");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_Users_UserId",
                table: "GroupUser");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "GroupUser",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "GroupUser",
                newName: "GroupsId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUser_UserId",
                table: "GroupUser",
                newName: "IX_GroupUser_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_Groups_GroupsId",
                table: "GroupUser",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_Users_UsersId",
                table: "GroupUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
