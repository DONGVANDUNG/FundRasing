using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esign.Migrations
{
    /// <inheritdoc />
    public partial class updateentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountJumpMax",
                table: "Auction");

            migrationBuilder.DropColumn(
                name: "AmountJumpMin",
                table: "Auction");

            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "Auction");

            migrationBuilder.DropColumn(
                name: "StartingPrice",
                table: "Auction");

            migrationBuilder.RenameColumn(
                name: "FundId",
                table: "FundImage",
                newName: "PostId");

            migrationBuilder.RenameColumn(
                name: "AuctionId",
                table: "AuctionImages",
                newName: "AuctionItemId");

            migrationBuilder.AddColumn<string>(
                name: "IntroduceOrganization",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FundRaiserPost",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    FinishDay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsClose = table.Column<bool>(type: "bit", nullable: true),
                    AmountOfMoneyTarget = table.Column<float>(type: "real", nullable: true),
                    FundId = table.Column<long>(type: "bigint", nullable: true),
                    AmountOfMoneyPresent = table.Column<float>(type: "real", nullable: true),
                    TargetIntroduce = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImplementationProgress = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_FundRaiserPost", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FundRaiserPost");

            migrationBuilder.DropColumn(
                name: "IntroduceOrganization",
                table: "AbpUsers");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "FundImage",
                newName: "FundId");

            migrationBuilder.RenameColumn(
                name: "AuctionItemId",
                table: "AuctionImages",
                newName: "AuctionId");

            migrationBuilder.AddColumn<int>(
                name: "AmountJumpMax",
                table: "Auction",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmountJumpMin",
                table: "Auction",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "Auction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartingPrice",
                table: "Auction",
                type: "int",
                nullable: true);
        }
    }
}
