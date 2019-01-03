using Microsoft.EntityFrameworkCore.Migrations;

namespace WebWallet.Data.Migrations
{
    public partial class fixtypoinrecurringpayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountRemainig",
                table: "RecurringPayments",
                newName: "AmountRemaining");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountRemaining",
                table: "RecurringPayments",
                newName: "AmountRemainig");
        }
    }
}
