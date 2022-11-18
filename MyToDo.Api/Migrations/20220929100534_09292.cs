using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyToDo.Api.Migrations
{
    public partial class _09292 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Setting",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Setting",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Setting",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Setting");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Setting");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Setting");
        }
    }
}
