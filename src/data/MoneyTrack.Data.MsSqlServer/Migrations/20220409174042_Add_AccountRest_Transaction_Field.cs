using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyTrack.Data.MsSqlServer.Migrations
{
    public partial class Add_AccountRest_Transaction_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPostponed",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Accounts");

            migrationBuilder.AddColumn<decimal>(
                name: "AccountRest",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "LastTransactionId",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_LastTransactionId",
                table: "Accounts",
                column: "LastTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Transactions_LastTransactionId",
                table: "Accounts",
                column: "LastTransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Transactions_LastTransactionId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_LastTransactionId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AccountRest",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "LastTransactionId",
                table: "Accounts");

            migrationBuilder.AddColumn<bool>(
                name: "IsPostponed",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Accounts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
