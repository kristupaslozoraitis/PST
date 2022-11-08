using Microsoft.EntityFrameworkCore.Migrations;

namespace KAMANDAX.Migrations
{
    public partial class AddedcolumnsaboutreceiverinformationtoOrderInformationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceiverCity",
                table: "OrderInformation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverCountry",
                table: "OrderInformation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverEmailAddress",
                table: "OrderInformation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverFullName",
                table: "OrderInformation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverHomeAddress",
                table: "OrderInformation",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverCity",
                table: "OrderInformation");

            migrationBuilder.DropColumn(
                name: "ReceiverCountry",
                table: "OrderInformation");

            migrationBuilder.DropColumn(
                name: "ReceiverEmailAddress",
                table: "OrderInformation");

            migrationBuilder.DropColumn(
                name: "ReceiverFullName",
                table: "OrderInformation");

            migrationBuilder.DropColumn(
                name: "ReceiverHomeAddress",
                table: "OrderInformation");
        }
    }
}
