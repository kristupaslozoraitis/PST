using Microsoft.EntityFrameworkCore.Migrations;

namespace KAMANDAX.Migrations
{
    public partial class AddedcolumnTotalPricetoOrderInformationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "OrderInformation",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "OrderInformation");
        }
    }
}
