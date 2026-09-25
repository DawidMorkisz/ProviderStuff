using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProviderStuff.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSubcontractorAndRestructurePingResults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PingResults_MonitoredAddresses_MonitoredAddressId",
                table: "PingResults");

            migrationBuilder.RenameColumn(
                name: "MonitoredAddressId",
                table: "PingResults",
                newName: "PingTestRunId");

            migrationBuilder.RenameIndex(
                name: "IX_PingResults_MonitoredAddressId_Timestamp",
                table: "PingResults",
                newName: "IX_PingResults_PingTestRunId_Timestamp");

            migrationBuilder.AddColumn<Guid>(
                name: "SubcontractorId",
                table: "MonitoredAddresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PingTestRun",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MonitoredAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RunAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPings = table.Column<int>(type: "int", nullable: false),
                    SuccessCount = table.Column<int>(type: "int", nullable: false),
                    PacketLossPercent = table.Column<double>(type: "float", nullable: false),
                    AverageResponseTimeMs = table.Column<double>(type: "float", nullable: false),
                    MinResponseTimeMs = table.Column<int>(type: "int", nullable: false),
                    MaxResponseTimeMs = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PingTestRun", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PingTestRun_MonitoredAddresses_MonitoredAddressId",
                        column: x => x.MonitoredAddressId,
                        principalTable: "MonitoredAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subcontractors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcontractors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubcontractorContactPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubcontractorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubcontractorContactPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubcontractorContactPoints_Subcontractors_SubcontractorId",
                        column: x => x.SubcontractorId,
                        principalTable: "Subcontractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonitoredAddresses_SubcontractorId",
                table: "MonitoredAddresses",
                column: "SubcontractorId");

            migrationBuilder.CreateIndex(
                name: "IX_PingTestRun_MonitoredAddressId",
                table: "PingTestRun",
                column: "MonitoredAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_SubcontractorContactPoints_SubcontractorId",
                table: "SubcontractorContactPoints",
                column: "SubcontractorId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonitoredAddresses_Subcontractors_SubcontractorId",
                table: "MonitoredAddresses",
                column: "SubcontractorId",
                principalTable: "Subcontractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_PingResults_PingTestRun_PingTestRunId",
                table: "PingResults",
                column: "PingTestRunId",
                principalTable: "PingTestRun",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonitoredAddresses_Subcontractors_SubcontractorId",
                table: "MonitoredAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_PingResults_PingTestRun_PingTestRunId",
                table: "PingResults");

            migrationBuilder.DropTable(
                name: "PingTestRun");

            migrationBuilder.DropTable(
                name: "SubcontractorContactPoints");

            migrationBuilder.DropTable(
                name: "Subcontractors");

            migrationBuilder.DropIndex(
                name: "IX_MonitoredAddresses_SubcontractorId",
                table: "MonitoredAddresses");

            migrationBuilder.DropColumn(
                name: "SubcontractorId",
                table: "MonitoredAddresses");

            migrationBuilder.RenameColumn(
                name: "PingTestRunId",
                table: "PingResults",
                newName: "MonitoredAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_PingResults_PingTestRunId_Timestamp",
                table: "PingResults",
                newName: "IX_PingResults_MonitoredAddressId_Timestamp");

            migrationBuilder.AddForeignKey(
                name: "FK_PingResults_MonitoredAddresses_MonitoredAddressId",
                table: "PingResults",
                column: "MonitoredAddressId",
                principalTable: "MonitoredAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
