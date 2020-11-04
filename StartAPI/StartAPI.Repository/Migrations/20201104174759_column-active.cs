using Microsoft.EntityFrameworkCore.Migrations;

namespace StartAPI.Repository.Migrations
{
    public partial class columnactive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Initals",
                table: "Departments");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Departments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Initials",
                table: "Departments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Initials",
                table: "Departments");

            migrationBuilder.AddColumn<string>(
                name: "Initals",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
