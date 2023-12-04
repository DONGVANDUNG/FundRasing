using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esign.Migrations
{
    /// <inheritdoc />
    public partial class updateautionitems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "AmountTargetOfMoney",
                table: "Auction",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LimitedOfNumber",
                table: "Auction",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountTargetOfMoney",
                table: "Auction");

            migrationBuilder.DropColumn(
                name: "LimitedOfNumber",
                table: "Auction");
        }
    }
}
