using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bk.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MtblBookEntriesADescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "tblBookEntries",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "tblBookEntries");
        }
    }
}
