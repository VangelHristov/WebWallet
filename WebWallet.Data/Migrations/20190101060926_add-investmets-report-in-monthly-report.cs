using Microsoft.EntityFrameworkCore.Migrations;

namespace WebWallet.Data.Migrations
{
    public partial class addinvestmetsreportinmonthlyreport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvestmentsPerTypeJson",
                table: "MonthlyReports",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalInvested",
                table: "MonthlyReports",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvestmentsPerTypeJson",
                table: "MonthlyReports");

            migrationBuilder.DropColumn(
                name: "TotalInvested",
                table: "MonthlyReports");
        }
    }
}
