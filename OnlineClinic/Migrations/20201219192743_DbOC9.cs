using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineClinic.Migrations
{
    public partial class DbOC9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "token_confirm_time",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "UserConfirmTokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    confirmToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConfirmTokens", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserConfirmTokens");

            migrationBuilder.AddColumn<DateTime>(
                name: "token_confirm_time",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
