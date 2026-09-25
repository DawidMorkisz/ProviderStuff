using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProviderStuff.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LocationAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactPoints_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonitoredAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    PingIntervalSeconds = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoredAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonitoredAddresses_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PingResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MonitoredAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    ResponseTimeMs = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PingResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PingResults_MonitoredAddresses_MonitoredAddressId",
                        column: x => x.MonitoredAddressId,
                        principalTable: "MonitoredAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatusChangeLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MonitoredAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusChangeLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusChangeLogs_MonitoredAddresses_MonitoredAddressId",
                        column: x => x.MonitoredAddressId,
                        principalTable: "MonitoredAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactPoints_ClientId",
                table: "ContactPoints",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoredAddresses_ClientId",
                table: "MonitoredAddresses",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PingResults_MonitoredAddressId_Timestamp",
                table: "PingResults",
                columns: new[] { "MonitoredAddressId", "Timestamp" });

            migrationBuilder.CreateIndex(
                name: "IX_StatusChangeLogs_MonitoredAddressId",
                table: "StatusChangeLogs",
                column: "MonitoredAddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactPoints");

            migrationBuilder.DropTable(
                name: "PingResults");

            migrationBuilder.DropTable(
                name: "StatusChangeLogs");

            migrationBuilder.DropTable(
                name: "MonitoredAddresses");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
