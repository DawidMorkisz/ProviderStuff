using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProviderStuff.Data.Migrations
{
    /// <inheritdoc />
    public partial class WorkerBackgroundLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PingTestRun_MonitoredAddressId",
                table: "PingTestRun");

            migrationBuilder.CreateIndex(
                name: "IX_PingTestRun_MonitoredAddressId_RunAt",
                table: "PingTestRun",
                columns: new[] { "MonitoredAddressId", "RunAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PingTestRun_MonitoredAddressId_RunAt",
                table: "PingTestRun");

            migrationBuilder.CreateIndex(
                name: "IX_PingTestRun_MonitoredAddressId",
                table: "PingTestRun",
                column: "MonitoredAddressId");
        }
    }
}
