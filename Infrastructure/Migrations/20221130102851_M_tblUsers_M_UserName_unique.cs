using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bk.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MtblUsersMUserNameunique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_tblUsers_UserName",
                table: "tblUsers",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tblUsers_UserName",
                table: "tblUsers");
        }
    }
}
