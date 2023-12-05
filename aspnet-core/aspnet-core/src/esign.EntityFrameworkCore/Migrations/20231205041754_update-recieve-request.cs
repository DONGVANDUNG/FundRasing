using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esign.Migrations
{
    /// <inheritdoc />
    public partial class updaterecieverequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApproveDate",
                table: "RequestToFundRaiser",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FundPackageId",
                table: "RequestToFundRaiser",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproveDate",
                table: "RequestToFundRaiser");

            migrationBuilder.DropColumn(
                name: "FundPackageId",
                table: "RequestToFundRaiser");
        }
    }
}
