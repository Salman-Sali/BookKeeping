using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bk.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtblBookEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblBookEntries",
                columns: table => new
                {
                    BookEntryId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Debit = table.Column<decimal>(type: "TEXT", nullable: true),
                    Credit = table.Column<decimal>(type: "TEXT", nullable: true),
                    Driver = table.Column<string>(type: "TEXT", nullable: true),
                    Vehicle = table.Column<string>(type: "TEXT", nullable: true),
                    Litre = table.Column<decimal>(type: "TEXT", nullable: true),
                    ItemType = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBookEntries", x => x.BookEntryId);
                    table.ForeignKey(
                        name: "FK_tblBookEntries_tblBooks_BookId",
                        column: x => x.BookId,
                        principalTable: "tblBooks",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblBookEntries_BookEntryId_Credit",
                table: "tblBookEntries",
                columns: new[] { "BookEntryId", "Credit" });

            migrationBuilder.CreateIndex(
                name: "IX_tblBookEntries_BookEntryId_Credit_Debit",
                table: "tblBookEntries",
                columns: new[] { "BookEntryId", "Credit", "Debit" });

            migrationBuilder.CreateIndex(
                name: "IX_tblBookEntries_BookEntryId_Debit",
                table: "tblBookEntries",
                columns: new[] { "BookEntryId", "Debit" });

            migrationBuilder.CreateIndex(
                name: "IX_tblBookEntries_BookId",
                table: "tblBookEntries",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBookEntries_Credit",
                table: "tblBookEntries",
                column: "Credit");

            migrationBuilder.CreateIndex(
                name: "IX_tblBookEntries_Debit",
                table: "tblBookEntries",
                column: "Debit");

            migrationBuilder.CreateIndex(
                name: "IX_tblBookEntries_Driver",
                table: "tblBookEntries",
                column: "Driver");

            migrationBuilder.CreateIndex(
                name: "IX_tblBookEntries_Vehicle",
                table: "tblBookEntries",
                column: "Vehicle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblBookEntries");
        }
    }
}
