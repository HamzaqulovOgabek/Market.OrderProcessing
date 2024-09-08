using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.OrderProcessing.Application.Migrations
{
    /// <inheritdoc />
    public partial class AddedStateToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "OrderItem",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "State",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Order");
        }
    }
}
