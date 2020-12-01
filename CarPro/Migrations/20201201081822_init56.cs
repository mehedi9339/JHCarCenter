using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPro.Migrations
{
    public partial class init56 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ac",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ac",
                table: "Customers");
        }
    }
}
