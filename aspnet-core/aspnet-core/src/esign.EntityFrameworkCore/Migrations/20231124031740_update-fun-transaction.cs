using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esign.Migrations
{
    /// <inheritdoc />
    public partial class updatefuntransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeTransaction",
                table: "FundTransactions");

            migrationBuilder.AddColumn<float>(
                name: "Balance",
                table: "FundTransactions",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "FundTransactions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ReceiverId",
                table: "FundTransactions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SenderId",
                table: "FundTransactions",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "FundTransactions");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "FundTransactions");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "FundTransactions");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "FundTransactions");

            migrationBuilder.AddColumn<int>(
                name: "TypeTransaction",
                table: "FundTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
