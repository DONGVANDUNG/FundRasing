using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esign.Migrations
{
    /// <inheritdoc />
    public partial class updatefundraiserpost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfMoneyPresent",
                table: "FundRaiserPost");

            migrationBuilder.DropColumn(
                name: "AmountOfMoneyTarget",
                table: "FundRaiserPost");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "FundRaiserPost",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purpose",
                table: "FundRaiserPost",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "FundRaiserPost");

            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "FundRaiserPost");

            migrationBuilder.AddColumn<float>(
                name: "AmountOfMoneyPresent",
                table: "FundRaiserPost",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AmountOfMoneyTarget",
                table: "FundRaiserPost",
                type: "real",
                nullable: true);
        }
    }
}
