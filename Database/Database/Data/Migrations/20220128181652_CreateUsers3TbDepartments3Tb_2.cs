using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Data.Migrations
{
    public partial class CreateUsers3TbDepartments3Tb_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users3",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users3", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments3",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ManagerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments3", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments3_Users3_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users3",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments3_ManagerId",
                table: "Departments3",
                column: "ManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments3");

            migrationBuilder.DropTable(
                name: "Users3");
        }
    }
}
