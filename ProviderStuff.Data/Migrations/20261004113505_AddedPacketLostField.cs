using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProviderStuff.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPacketLostField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PacketLost",
                table: "PingTestRuns",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PacketLost",
                table: "PingTestRuns");
        }
    }
}
