using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esign.Migrations
{
    /// <inheritdoc />
    public partial class changecolumnstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClose",
                table: "AuctionItems");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "AuctionItems",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "AuctionItems");

            migrationBuilder.AddColumn<bool>(
                name: "IsClose",
                table: "AuctionItems",
                type: "bit",
                nullable: true);
        }
    }
}
