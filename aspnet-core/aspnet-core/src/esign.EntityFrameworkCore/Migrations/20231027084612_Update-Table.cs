using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esign.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FundTransactions");

            migrationBuilder.AddColumn<string>(
                name: "EmailReceiver",
                table: "FundTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailSender",
                table: "FundTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FundDetailContent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FundId = table.Column<int>(type: "int", nullable: false),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonCreatedFund = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdeaCreadtedFund = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Footer = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_FundDetailContent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FundRaisingTopic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FundId = table.Column<int>(type: "int", nullable: false),
                    TopicName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripton = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_FundRaisingTopic", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FundDetailContent");

            migrationBuilder.DropTable(
                name: "FundRaisingTopic");

            migrationBuilder.DropColumn(
                name: "EmailReceiver",
                table: "FundTransactions");

            migrationBuilder.DropColumn(
                name: "EmailSender",
                table: "FundTransactions");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "FundTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
