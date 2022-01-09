using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoListApiSqlite.Migrations
{
    public partial class GroupModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name2",
                table: "Groups");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name2",
                table: "Groups",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
