using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bk.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtblUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUsers", x => x.UserId);
                });

            migrationBuilder.Sql($"INSERT INTO tblUsers\r\n(UserName, Password, CreatedBy, UpdatedBy, CreatedOn, UpdatedOn)\r\nVALUES('Admin', 'Password', 1, 1, '{DateTime.Now}', '{DateTime.Now}');\r\n");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblUsers");
        }
    }
}
