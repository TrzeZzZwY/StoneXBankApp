using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoneXBankApp.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surename = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Login = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Pesel = table.Column<string>(type: "TEXT", nullable: false),
                    BirtDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProfileId = table.Column<int>(type: "INTEGER", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "TEXT", nullable: false),
                    CrateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Balance = table.Column<decimal>(type: "TEXT", nullable: false),
                    CurrencyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bankAccounts_currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bankAccounts_profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "savedContactData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surename = table.Column<string>(type: "TEXT", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "TEXT", nullable: false),
                    ProfileId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_savedContactData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_savedContactData_profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    senderId = table.Column<int>(type: "INTEGER", nullable: false),
                    recipientId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amout = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transactions_bankAccounts_recipientId",
                        column: x => x.recipientId,
                        principalTable: "bankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transactions_bankAccounts_senderId",
                        column: x => x.senderId,
                        principalTable: "bankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bankAccounts_CurrencyId",
                table: "bankAccounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_bankAccounts_ProfileId",
                table: "bankAccounts",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_savedContactData_ProfileId",
                table: "savedContactData",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_recipientId",
                table: "transactions",
                column: "recipientId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_senderId",
                table: "transactions",
                column: "senderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "savedContactData");

            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "bankAccounts");

            migrationBuilder.DropTable(
                name: "currencies");

            migrationBuilder.DropTable(
                name: "profiles");
        }
    }
}
