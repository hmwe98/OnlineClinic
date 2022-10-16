using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineClinic.Migrations
{
    public partial class DbOC6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "patientId",
                table: "Patients");

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Patients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "Patients");

            migrationBuilder.AddColumn<string>(
                name: "patientId",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
