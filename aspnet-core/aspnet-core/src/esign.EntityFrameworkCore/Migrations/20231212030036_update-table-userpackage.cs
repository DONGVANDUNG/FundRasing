using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esign.Migrations
{
    /// <inheritdoc />
    public partial class updatetableuserpackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balances",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "FundPackageId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "FundRaiserDate",
                table: "AbpUsers");

            migrationBuilder.CreateTable(
                name: "UserFundPackage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    FundPackageId = table.Column<long>(type: "bigint", nullable: true),
                    IsExpired = table.Column<bool>(type: "bit", nullable: true),
                    FundEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_UserFundPackage", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFundPackage");

            migrationBuilder.AddColumn<float>(
                name: "Balances",
                table: "AbpUsers",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FundPackageId",
                table: "AbpUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FundRaiserDate",
                table: "AbpUsers",
                type: "datetime2",
                nullable: true);
        }
    }
}
