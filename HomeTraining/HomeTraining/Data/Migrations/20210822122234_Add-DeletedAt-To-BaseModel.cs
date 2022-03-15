using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeTraining.Data.Migrations
{
    public partial class AddDeletedAtToBaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletededAt",
                table: "Employees",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletededAt",
                table: "Departments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletededAt",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DeletededAt",
                table: "Departments");
        }
    }
}
