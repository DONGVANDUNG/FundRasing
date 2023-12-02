using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esign.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase3011 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AbpUsers");

            migrationBuilder.RenameColumn(
                name: "FundRaiserId",
                table: "Funds",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "TitlePart",
                table: "FundDetails",
                newName: "Target");

            migrationBuilder.RenameColumn(
                name: "ContentPart",
                table: "FundDetails",
                newName: "Purpose");

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

            migrationBuilder.AlterColumn<float>(
                name: "Commission",
                table: "Funds",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "AmountOfMoney",
                table: "Funds",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<float>(
                name: "AmountDonationPresent",
                table: "Funds",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AmountDonationTarget",
                table: "Funds",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DonateAmount",
                table: "Funds",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostTopic",
                table: "FundRaiserPost",
                type: "nvarchar(max)",
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

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "FundDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationName",
                table: "FundDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountDonationPresent",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "AmountDonationTarget",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "DonateAmount",
                table: "Funds");

            migrationBuilder.DropColumn(
                name: "PostTopic",
                table: "FundRaiserPost");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "FundDetails");

            migrationBuilder.DropColumn(
                name: "OrganizationName",
                table: "FundDetails");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Funds",
                newName: "FundRaiserId");

            migrationBuilder.RenameColumn(
                name: "Target",
                table: "FundDetails",
                newName: "TitlePart");

            migrationBuilder.RenameColumn(
                name: "Purpose",
                table: "FundDetails",
                newName: "ContentPart");

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

            migrationBuilder.AlterColumn<float>(
                name: "Commission",
                table: "Funds",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "AmountOfMoney",
                table: "Funds",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FundDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
