using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletCash.Migrations
{
    /// <inheritdoc />
    public partial class InitialCashOut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cashout");

            migrationBuilder.CreateTable(
                name: "References",
                schema: "cashout",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CustomData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirationMinutes = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CancelledAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                schema: "cashout",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DappNotificationId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ReferenceId = table.Column<long>(type: "bigint", nullable: true),
                    ReferenceText = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Merchant = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Refunded = table.Column<bool>(type: "bit", nullable: false),
                    NotificationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CustomData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SecurityVersion = table.Column<int>(type: "int", nullable: true),
                    SignatureValid = table.Column<bool>(type: "bit", nullable: false),
                    ResultCode = table.Column<int>(type: "int", nullable: false),
                    ResultMessage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReceivedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    ProcessedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_References_ReferenceId",
                        column: x => x.ReferenceId,
                        principalSchema: "cashout",
                        principalTable: "References",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_DappNotificationId",
                schema: "cashout",
                table: "Notifications",
                column: "DappNotificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ReferenceId",
                schema: "cashout",
                table: "Notifications",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_References_Reference",
                schema: "cashout",
                table: "References",
                column: "Reference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_References_Status",
                schema: "cashout",
                table: "References",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications",
                schema: "cashout");

            migrationBuilder.DropTable(
                name: "References",
                schema: "cashout");
        }
    }
}
