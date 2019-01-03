using Microsoft.EntityFrameworkCore.Migrations;

namespace WebWallet.Data.Migrations
{
    public partial class removeTransactionspropertyfromallentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Goals_GoalId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Investments_InvestmentId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_GoalId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_InvestmentId",
                table: "Transactions");

            migrationBuilder.AlterColumn<string>(
                name: "InvestmentId",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GoalId",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InvestmentId",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GoalId",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_GoalId",
                table: "Transactions",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_InvestmentId",
                table: "Transactions",
                column: "InvestmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Goals_GoalId",
                table: "Transactions",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Investments_InvestmentId",
                table: "Transactions",
                column: "InvestmentId",
                principalTable: "Investments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
