using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esign.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTable1207 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FundContentId",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "Footer",
                table: "FundDetailContent");

            migrationBuilder.RenameColumn(
                name: "IdeaCreadtedFund",
                table: "FundDetailContent",
                newName: "Content");

            migrationBuilder.AddColumn<bool>(
                name: "IsPayFee",
                table: "Funds",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "FundImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FundId = table.Column<long>(type: "bigint", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_FundImage", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FundImage");

            migrationBuilder.DropColumn(
                name: "IsPayFee",
                table: "Funds");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "FundDetailContent",
                newName: "IdeaCreadtedFund");

            migrationBuilder.AddColumn<int>(
                name: "FundContentId",
                table: "Funds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Footer",
                table: "FundDetailContent",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
