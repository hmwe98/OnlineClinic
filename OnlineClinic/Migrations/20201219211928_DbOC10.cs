using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineClinic.Migrations
{
    public partial class DbOC10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserConfirmTokens");

            migrationBuilder.AddColumn<string>(
                name: "confirmEmailtoken",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "confirmEmailtoken",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "UserConfirmTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    confirmToken = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConfirmTokens", x => x.Id);
                });
        }
    }
}
