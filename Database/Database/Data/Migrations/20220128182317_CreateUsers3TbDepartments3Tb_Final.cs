using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Data.Migrations
{
    public partial class CreateUsers3TbDepartments3Tb_Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments3_Users3_ManagerId",
                table: "Departments3");

            migrationBuilder.DropIndex(
                name: "IX_Departments3_ManagerId",
                table: "Departments3");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerId",
                table: "Departments3",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments3_ManagerId",
                table: "Departments3",
                column: "ManagerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments3_Users3_ManagerId",
                table: "Departments3",
                column: "ManagerId",
                principalTable: "Users3",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments3_Users3_ManagerId",
                table: "Departments3");

            migrationBuilder.DropIndex(
                name: "IX_Departments3_ManagerId",
                table: "Departments3");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerId",
                table: "Departments3",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Departments3_ManagerId",
                table: "Departments3",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments3_Users3_ManagerId",
                table: "Departments3",
                column: "ManagerId",
                principalTable: "Users3",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
