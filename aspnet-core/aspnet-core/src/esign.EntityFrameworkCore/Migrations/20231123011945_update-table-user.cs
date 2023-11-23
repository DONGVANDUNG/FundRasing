using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esign.Migrations
{
    /// <inheritdoc />
    public partial class updatetableuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FundPackageId",
                table: "AbpUsers");

            migrationBuilder.AlterColumn<int>(
                name: "FundPackageId",
                table: "FundRaiser",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Blances",
                table: "AbpUsers",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Blances",
                table: "AbpUsers");

            migrationBuilder.AlterColumn<long>(
                name: "FundPackageId",
                table: "FundRaiser",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FundPackageId",
                table: "AbpUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
