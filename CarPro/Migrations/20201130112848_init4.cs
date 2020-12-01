using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPro.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptNo",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ReceiptNo",
                table: "Orders");
        }
    }
}
