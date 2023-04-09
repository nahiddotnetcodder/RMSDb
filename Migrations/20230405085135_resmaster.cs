using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    public partial class resmaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResSalesMasterRSMId",
                table: "HREmpDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ResDClose",
                columns: table => new
                {
                    RDCId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RDCDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResDClose", x => x.RDCId);
                });

            migrationBuilder.CreateTable(
                name: "ResSalesMaster",
                columns: table => new
                {
                    RSMId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RSMTableNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RDCId = table.Column<int>(type: "int", nullable: false),
                    RDCDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HREDId = table.Column<int>(type: "int", nullable: false),
                    RSMTime = table.Column<int>(type: "int", nullable: false),
                    RSMPerson = table.Column<int>(type: "int", nullable: false),
                    IsBPrint = table.Column<bool>(type: "bit", nullable: false),
                    IsBPaid = table.Column<bool>(type: "bit", nullable: false),
                    CUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResSalesMaster", x => x.RSMId);
                    table.ForeignKey(
                        name: "FK_ResSalesMaster_HREmpDetails_HREDId",
                        column: x => x.HREDId,
                        principalTable: "HREmpDetails",
                        principalColumn: "HREDId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResSalesDetails",
                columns: table => new
                {
                    RSDId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RSMId = table.Column<int>(type: "int", nullable: false),
                    RMId = table.Column<int>(type: "int", nullable: false),
                    RSDUPrice = table.Column<double>(type: "float", nullable: false),
                    RSDQty = table.Column<int>(type: "int", nullable: false),
                    RSDTotalPrice = table.Column<double>(type: "float", nullable: false),
                    RSDKotNo = table.Column<int>(type: "int", nullable: false),
                    IsCancel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResSalesDetails", x => x.RSDId);
                    table.ForeignKey(
                        name: "FK_ResSalesDetails_ResMenu_RMId",
                        column: x => x.RMId,
                        principalTable: "ResMenu",
                        principalColumn: "RMId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResSalesDetails_ResSalesMaster_RSMId",
                        column: x => x.RSMId,
                        principalTable: "ResSalesMaster",
                        principalColumn: "RSMId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HREmpDetails_ResSalesMasterRSMId",
                table: "HREmpDetails",
                column: "ResSalesMasterRSMId");

            migrationBuilder.CreateIndex(
                name: "IX_ResSalesDetails_RMId",
                table: "ResSalesDetails",
                column: "RMId");

            migrationBuilder.CreateIndex(
                name: "IX_ResSalesDetails_RSMId",
                table: "ResSalesDetails",
                column: "RSMId");

            migrationBuilder.CreateIndex(
                name: "IX_ResSalesMaster_HREDId",
                table: "ResSalesMaster",
                column: "HREDId");

            migrationBuilder.AddForeignKey(
                name: "FK_HREmpDetails_ResSalesMaster_ResSalesMasterRSMId",
                table: "HREmpDetails",
                column: "ResSalesMasterRSMId",
                principalTable: "ResSalesMaster",
                principalColumn: "RSMId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HREmpDetails_ResSalesMaster_ResSalesMasterRSMId",
                table: "HREmpDetails");

            migrationBuilder.DropTable(
                name: "ResDClose");

            migrationBuilder.DropTable(
                name: "ResSalesDetails");

            migrationBuilder.DropTable(
                name: "ResSalesMaster");

            migrationBuilder.DropIndex(
                name: "IX_HREmpDetails_ResSalesMasterRSMId",
                table: "HREmpDetails");

            migrationBuilder.DropColumn(
                name: "ResSalesMasterRSMId",
                table: "HREmpDetails");
        }
    }
}
