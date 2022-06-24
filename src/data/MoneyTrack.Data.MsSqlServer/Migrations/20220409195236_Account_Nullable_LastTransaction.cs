using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyTrack.Data.MsSqlServer.Migrations
{
    public partial class Account_Nullable_LastTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Transactions_LastTransactionId",
                table: "Accounts");

            migrationBuilder.AlterColumn<int>(
                name: "LastTransactionId",
                table: "Accounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Transactions_LastTransactionId",
                table: "Accounts",
                column: "LastTransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Transactions_LastTransactionId",
                table: "Accounts");

            migrationBuilder.AlterColumn<int>(
                name: "LastTransactionId",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Transactions_LastTransactionId",
                table: "Accounts",
                column: "LastTransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
