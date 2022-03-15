using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Data.Migrations
{
    public partial class DbInt_CreateUser1TbDepartment1Tb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users1",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments1",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ManagerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments1_Users1_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments1_ManagerId",
                table: "Departments1",
                column: "ManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments1");

            migrationBuilder.DropTable(
                name: "Users1");
        }
    }
}
