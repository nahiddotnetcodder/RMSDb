using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    public partial class empName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HREmpDetails_ResSalesMaster_ResSalesMasterRSMId",
                table: "HREmpDetails");

            migrationBuilder.DropIndex(
                name: "IX_HREmpDetails_ResSalesMasterRSMId",
                table: "HREmpDetails");

            migrationBuilder.DropColumn(
                name: "ResSalesMasterRSMId",
                table: "HREmpDetails");

            migrationBuilder.AddColumn<string>(
                name: "MyProperty",
                table: "ResSalesMaster",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "ResSalesMaster");

            migrationBuilder.AddColumn<int>(
                name: "ResSalesMasterRSMId",
                table: "HREmpDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HREmpDetails_ResSalesMasterRSMId",
                table: "HREmpDetails",
                column: "ResSalesMasterRSMId");

            migrationBuilder.AddForeignKey(
                name: "FK_HREmpDetails_ResSalesMaster_ResSalesMasterRSMId",
                table: "HREmpDetails",
                column: "ResSalesMasterRSMId",
                principalTable: "ResSalesMaster",
                principalColumn: "RSMId");
        }
    }
}
