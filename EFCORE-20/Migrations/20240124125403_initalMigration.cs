using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCORE20.Migrations
{
    /// <inheritdoc />
    public partial class initalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountHolder = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "GLTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BankAccountAccountId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GLTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GLTransactions_BankAccounts_BankAccountAccountId",
                        column: x => x.BankAccountAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "AccountId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GLTransactions_BankAccountAccountId",
                table: "GLTransactions",
                column: "BankAccountAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GLTransactions");

            migrationBuilder.DropTable(
                name: "BankAccounts");
        }
    }
}
