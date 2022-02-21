using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CheckoutManagement.Infrastructure.data.migrations
{
    public partial class Makecustomernameunique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Customers_Name",
                table: "Customers",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_Name",
                table: "Customers");
        }
    }
}
