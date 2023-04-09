using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    public partial class recManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RMDetails_RMMaster_RMMId",
                table: "RMDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RMMaster",
                table: "RMMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RMDetails",
                table: "RMDetails");

            migrationBuilder.RenameTable(
                name: "RMMaster",
                newName: "RecMMaster");

            migrationBuilder.RenameTable(
                name: "RMDetails",
                newName: "RecMDetails");

            migrationBuilder.RenameIndex(
                name: "IX_RMDetails_RMMId",
                table: "RecMDetails",
                newName: "IX_RecMDetails_RMMId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecMMaster",
                table: "RecMMaster",
                column: "RMMId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecMDetails",
                table: "RecMDetails",
                column: "RMDId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecMDetails_RecMMaster_RMMId",
                table: "RecMDetails",
                column: "RMMId",
                principalTable: "RecMMaster",
                principalColumn: "RMMId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecMDetails_RecMMaster_RMMId",
                table: "RecMDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecMMaster",
                table: "RecMMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecMDetails",
                table: "RecMDetails");

            migrationBuilder.RenameTable(
                name: "RecMMaster",
                newName: "RMMaster");

            migrationBuilder.RenameTable(
                name: "RecMDetails",
                newName: "RMDetails");

            migrationBuilder.RenameIndex(
                name: "IX_RecMDetails_RMMId",
                table: "RMDetails",
                newName: "IX_RMDetails_RMMId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RMMaster",
                table: "RMMaster",
                column: "RMMId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RMDetails",
                table: "RMDetails",
                column: "RMDId");

            migrationBuilder.AddForeignKey(
                name: "FK_RMDetails_RMMaster_RMMId",
                table: "RMDetails",
                column: "RMMId",
                principalTable: "RMMaster",
                principalColumn: "RMMId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
