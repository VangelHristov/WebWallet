using Microsoft.EntityFrameworkCore.Migrations;

namespace WebWallet.Data.Migrations
{
    public partial class updateentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_RecurringPayments_RecurringPaymentId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_RecurringPaymentId",
                table: "Transactions");

            migrationBuilder.AlterColumn<string>(
                name: "RecurringPaymentId",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountRemainig",
                table: "RecurringPayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Remaining",
                table: "Goals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountRemainig",
                table: "RecurringPayments");

            migrationBuilder.DropColumn(
                name: "Remaining",
                table: "Goals");

            migrationBuilder.AlterColumn<string>(
                name: "RecurringPaymentId",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RecurringPaymentId",
                table: "Transactions",
                column: "RecurringPaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_RecurringPayments_RecurringPaymentId",
                table: "Transactions",
                column: "RecurringPaymentId",
                principalTable: "RecurringPayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
