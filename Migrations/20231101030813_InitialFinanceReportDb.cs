using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finance_reporter_api.Migrations
{
    /// <inheritdoc />
    public partial class InitialFinanceReportDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogDataExchange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MessageSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageTarget = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MethodCall = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessagePayload = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogDataExchange", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogException",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExceptionMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExceptionStackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InnerExceptionMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InnerExceptionStackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExceptionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CallingMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CallingAssemblyInfo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogException", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogTrace",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogTrace", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FontSize = table.Column<long>(type: "bigint", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Messages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DarkMode = table.Column<bool>(type: "bit", nullable: false),
                    SidenavMini = table.Column<bool>(type: "bit", nullable: false),
                    NavbarFixed = table.Column<bool>(type: "bit", nullable: false),
                    SidenavType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogDataExchange");

            migrationBuilder.DropTable(
                name: "LogException");

            migrationBuilder.DropTable(
                name: "LogTrace");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserSettings");
        }
    }
}
