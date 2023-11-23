using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esign.Migrations
{
    /// <inheritdoc />
    public partial class updatetablefundtransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailSender",
                table: "FundTransactions",
                newName: "Sender");

            migrationBuilder.RenameColumn(
                name: "EmailReceiver",
                table: "FundTransactions",
                newName: "Receiver");

            migrationBuilder.AddColumn<float>(
                name: "Commission",
                table: "FundTransactions",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Commission",
                table: "Funds",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Commission",
                table: "FundPackage",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Commission",
                table: "FundTransactions");

            migrationBuilder.DropColumn(
                name: "Commission",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "Commission",
                table: "FundPackage");

            migrationBuilder.RenameColumn(
                name: "Sender",
                table: "FundTransactions",
                newName: "EmailSender");

            migrationBuilder.RenameColumn(
                name: "Receiver",
                table: "FundTransactions",
                newName: "EmailReceiver");
        }
    }
}
