using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebWallet.Data.Migrations
{
    public partial class addmonthlyreporttocontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonthlyReports",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    EndBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalSpendings = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SpendingsPerCategoryJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyReports", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonthlyReports");
        }
    }
}