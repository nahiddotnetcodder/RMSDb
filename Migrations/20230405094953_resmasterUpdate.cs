using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    public partial class resmasterUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResSalesDetails_ResMenu_RMId",
                table: "ResSalesDetails");

            migrationBuilder.DropIndex(
                name: "IX_ResSalesDetails_RMId",
                table: "ResSalesDetails");

            migrationBuilder.DropColumn(
                name: "RMId",
                table: "ResSalesDetails");

            migrationBuilder.AddColumn<string>(
                name: "RSDItemCode",
                table: "ResSalesDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RSDItemName",
                table: "ResSalesDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RSDItemCode",
                table: "ResSalesDetails");

            migrationBuilder.DropColumn(
                name: "RSDItemName",
                table: "ResSalesDetails");

            migrationBuilder.AddColumn<int>(
                name: "RMId",
                table: "ResSalesDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ResSalesDetails_RMId",
                table: "ResSalesDetails",
                column: "RMId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResSalesDetails_ResMenu_RMId",
                table: "ResSalesDetails",
                column: "RMId",
                principalTable: "ResMenu",
                principalColumn: "RMId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
