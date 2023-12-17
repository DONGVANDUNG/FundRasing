using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esign.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase0112 : Migration
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

            


            migrationBuilder.RenameColumn(
                name: "PaymenFee",
                table: "FundPackage",
                newName: "PaymentFee");


            migrationBuilder.RenameColumn(
                name: "Blances",
                table: "AbpUsers",
                newName: "Balances");

            migrationBuilder.AddColumn<long>(
                name: "AuctionItemId",
                table: "InformationReceiveAuction",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "FundTransactions",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Funds",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPayFee",
                table: "Funds",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsOutStanding",
                table: "Funds",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");


            migrationBuilder.AddColumn<float>(
                name: "PercentAchieved",
                table: "Funds",
                type: "real",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "FundDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");
            migrationBuilder.AddColumn<float>(
                name: "DonateSuggestAmount",
                table: "Funds",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfParticipants",
                table: "Auction",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuctionItemId",
                table: "InformationReceiveAuction");

            migrationBuilder.DropColumn(
                name: "PercentAchieved",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "FundDetails");

            migrationBuilder.DropColumn(
                name: "OrganizationName",
                table: "FundDetails");

            migrationBuilder.DropColumn(
                name: "NumberOfParticipants",
                table: "Auction");


            migrationBuilder.RenameColumn(
                name: "PaymentFee",
                table: "FundPackage",
                newName: "PaymenFee");


            migrationBuilder.RenameColumn(
                name: "Balances",
                table: "AbpUsers",
                newName: "Blances");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FundTransactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Funds",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsPayFee",
                table: "Funds",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsOutStanding",
                table: "Funds",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AmountOfMoney",
                table: "Funds",
                type: "real",
                nullable: false,
                defaultValue: 0f);
            migrationBuilder.DropColumn(
                name: "DonateSuggestAmount",
                table: "Funds");

            migrationBuilder.AddColumn<float>(
                name: "Commission",
                table: "Funds",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FundDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

        }
    }
}
