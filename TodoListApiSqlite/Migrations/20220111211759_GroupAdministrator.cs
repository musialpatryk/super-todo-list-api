using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoListApiSqlite.Migrations
{
    public partial class GroupAdministrator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdministratorId",
                table: "Groups",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_AdministratorId",
                table: "Groups",
                column: "AdministratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_AdministratorId",
                table: "Groups",
                column: "AdministratorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_AdministratorId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_AdministratorId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "AdministratorId",
                table: "Groups");
        }
    }
}
