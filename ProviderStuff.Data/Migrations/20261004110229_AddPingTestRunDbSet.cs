using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProviderStuff.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPingTestRunDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PingResults_PingTestRun_PingTestRunId",
                table: "PingResults");

            migrationBuilder.DropForeignKey(
                name: "FK_PingTestRun_MonitoredAddresses_MonitoredAddressId",
                table: "PingTestRun");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PingTestRun",
                table: "PingTestRun");

            migrationBuilder.RenameTable(
                name: "PingTestRun",
                newName: "PingTestRuns");

            migrationBuilder.RenameIndex(
                name: "IX_PingTestRun_MonitoredAddressId_RunAt",
                table: "PingTestRuns",
                newName: "IX_PingTestRuns_MonitoredAddressId_RunAt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PingTestRuns",
                table: "PingTestRuns",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PingResults_PingTestRuns_PingTestRunId",
                table: "PingResults",
                column: "PingTestRunId",
                principalTable: "PingTestRuns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PingTestRuns_MonitoredAddresses_MonitoredAddressId",
                table: "PingTestRuns",
                column: "MonitoredAddressId",
                principalTable: "MonitoredAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PingResults_PingTestRuns_PingTestRunId",
                table: "PingResults");

            migrationBuilder.DropForeignKey(
                name: "FK_PingTestRuns_MonitoredAddresses_MonitoredAddressId",
                table: "PingTestRuns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PingTestRuns",
                table: "PingTestRuns");

            migrationBuilder.RenameTable(
                name: "PingTestRuns",
                newName: "PingTestRun");

            migrationBuilder.RenameIndex(
                name: "IX_PingTestRuns_MonitoredAddressId_RunAt",
                table: "PingTestRun",
                newName: "IX_PingTestRun_MonitoredAddressId_RunAt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PingTestRun",
                table: "PingTestRun",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PingResults_PingTestRun_PingTestRunId",
                table: "PingResults",
                column: "PingTestRunId",
                principalTable: "PingTestRun",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PingTestRun_MonitoredAddresses_MonitoredAddressId",
                table: "PingTestRun",
                column: "MonitoredAddressId",
                principalTable: "MonitoredAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
