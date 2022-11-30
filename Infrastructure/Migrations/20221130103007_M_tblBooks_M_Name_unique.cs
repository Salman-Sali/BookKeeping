using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bk.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MtblBooksMNameunique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_tblBooks_Name",
                table: "tblBooks",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tblBooks_Name",
                table: "tblBooks");
        }
    }
}
