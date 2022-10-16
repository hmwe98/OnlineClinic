using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineClinic.Migrations
{
    public partial class DbOC5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "username",
                table: "Patients");

            migrationBuilder.AddColumn<string>(
                name: "patientId",
                table: "Patients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "patientId",
                table: "Patients");

            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
