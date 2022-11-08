using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KAMANDAX.Migrations
{
    public partial class AddedcolumnProductUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductUserId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductUserId",
                table: "Products");
        }
    }
}
