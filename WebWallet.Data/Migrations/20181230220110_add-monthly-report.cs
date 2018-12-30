using Microsoft.EntityFrameworkCore.Migrations;

namespace WebWallet.Data.Migrations
{
    public partial class addmonthlyreport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainCategory",
                table: "Transactions",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainCategory",
                table: "Transactions");
        }
    }
}
