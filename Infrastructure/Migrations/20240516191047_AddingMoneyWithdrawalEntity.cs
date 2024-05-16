using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddingMoneyWithdrawalEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MoneyWithdrawals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MoneyWithdrawalUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MoneyWithdrawalCurrencyCode = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyWithdrawals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoneyWithdrawals_Currencies_MoneyWithdrawalCurrencyCode",
                        column: x => x.MoneyWithdrawalCurrencyCode,
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoneyWithdrawals_Users_MoneyWithdrawalUserId",
                        column: x => x.MoneyWithdrawalUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoneyWithdrawals_MoneyWithdrawalCurrencyCode",
                table: "MoneyWithdrawals",
                column: "MoneyWithdrawalCurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyWithdrawals_MoneyWithdrawalUserId",
                table: "MoneyWithdrawals",
                column: "MoneyWithdrawalUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoneyWithdrawals");
        }
    }
}
