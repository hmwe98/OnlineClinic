using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineClinic.Migrations
{
    public partial class DbOC7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "img",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "img",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
