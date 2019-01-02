using Microsoft.EntityFrameworkCore.Migrations;

namespace WebWallet.Data.Migrations
{
    public partial class changereportcategoryspendings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpendingsPerMainCategoryJson",
                table: "MonthlyReports");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpendingsPerMainCategoryJson",
                table: "MonthlyReports",
                nullable: true);
        }
    }
}
