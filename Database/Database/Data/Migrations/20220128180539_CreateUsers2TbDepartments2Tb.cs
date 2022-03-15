using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Data.Migrations
{
    public partial class CreateUsers2TbDepartments2Tb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users2",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments2",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ManagerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments2_Users2_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments2_ManagerId",
                table: "Departments2",
                column: "ManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments2");

            migrationBuilder.DropTable(
                name: "Users2");
        }
    }
}
