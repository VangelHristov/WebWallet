using Microsoft.EntityFrameworkCore.Migrations;

namespace WebWallet.Data.Migrations
{
    public partial class addinvestmentabbrevitin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CryptoAbbreviation",
                table: "Investments");

            migrationBuilder.DropColumn(
                name: "CurrencyAbbreviation",
                table: "Investments");

            migrationBuilder.DropColumn(
                name: "StockAbbreviation",
                table: "Investments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CryptoAbbreviation",
                table: "Investments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyAbbreviation",
                table: "Investments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StockAbbreviation",
                table: "Investments",
                nullable: true);
        }
    }
}
