using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace store_api.Migrations
{
    public partial class store : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "products",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
