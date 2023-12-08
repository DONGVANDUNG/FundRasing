using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esign.Migrations
{
    /// <inheritdoc />
    public partial class updatetable812 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "FundRaiserPost");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "FundRaiserPost",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
