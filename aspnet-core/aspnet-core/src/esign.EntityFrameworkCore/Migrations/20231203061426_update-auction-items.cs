using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esign.Migrations
{
    /// <inheritdoc />
    public partial class updateauctionitems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfMoney",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "Commission",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "DonateSuggestAmount",
                table: "Funds");

            migrationBuilder.CreateTable(
                name: "AuctionItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuctionId = table.Column<long>(type: "bigint", nullable: true),
                    IntroduceItem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountJumpMin = table.Column<float>(type: "real", nullable: true),
                    AmountJumpMax = table.Column<float>(type: "real", nullable: true),
                    StartingPrice = table.Column<float>(type: "real", nullable: true),
                    AuctionPresentAmount = table.Column<float>(type: "real", nullable: true),
                    TargetAmountOfMoney = table.Column<float>(type: "real", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionItems", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuctionItems");

            migrationBuilder.AddColumn<float>(
                name: "AmountOfMoney",
                table: "Funds",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Commission",
                table: "Funds",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "DonateSuggestAmount",
                table: "Funds",
                type: "real",
                nullable: true);
        }
    }
}
