using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esign.Migrations
{
    /// <inheritdoc />
    public partial class updatetableauction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountMax",
                table: "Auction");

            migrationBuilder.DropColumn(
                name: "ImageUrlItem",
                table: "Auction");

            migrationBuilder.RenameColumn(
                name: "AmountMin",
                table: "Auction",
                newName: "AmountJumpMax");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountJumpMax",
                table: "Auction",
                newName: "AmountMin");

            migrationBuilder.AddColumn<int>(
                name: "AmountMax",
                table: "Auction",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrlItem",
                table: "Auction",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
