using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esign.Migrations
{
    /// <inheritdoc />
    public partial class updatestatusfundraising : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClose",
                table: "FundRaiserPost");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "FundRaiserPost",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "FundRaiserPost");

            migrationBuilder.AddColumn<bool>(
                name: "IsClose",
                table: "FundRaiserPost",
                type: "bit",
                nullable: true);
        }
    }
}
