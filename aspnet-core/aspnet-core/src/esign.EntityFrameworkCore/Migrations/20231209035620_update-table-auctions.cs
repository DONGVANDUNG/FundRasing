using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esign.Migrations
{
    /// <inheritdoc />
    public partial class updatetableauctions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuctionId",
                table: "AuctionTransactions",
                newName: "AuctionItemId");

            migrationBuilder.RenameColumn(
                name: "AuctionId",
                table: "AuctionItems",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AuctionId",
                table: "AuctionDeposit",
                newName: "AuctionItemId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "AuctionItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsClose",
                table: "AuctionItems",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "AuctionItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleAuction",
                table: "AuctionItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AuctionItemId",
                table: "AuctionImages",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "AuctionItems");

            migrationBuilder.DropColumn(
                name: "IsClose",
                table: "AuctionItems");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "AuctionItems");

            migrationBuilder.DropColumn(
                name: "TitleAuction",
                table: "AuctionItems");

            migrationBuilder.RenameColumn(
                name: "AuctionItemId",
                table: "AuctionTransactions",
                newName: "AuctionId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AuctionItems",
                newName: "AuctionId");

            migrationBuilder.RenameColumn(
                name: "AuctionItemId",
                table: "AuctionDeposit",
                newName: "AuctionId");

            migrationBuilder.AlterColumn<long>(
                name: "AuctionItemId",
                table: "AuctionImages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
