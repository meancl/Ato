using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AtoIndicator.Migrations
{
    public partial class mig_add_minutehistories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "minutehistories",
                columns: table => new
                {
                    compLoc = table.Column<int>(nullable: false),
                    sDate = table.Column<DateTime>(nullable: false),
                    sCode = table.Column<string>(nullable: false),
                    sCodeName = table.Column<string>(nullable: false),
                    nIdx = table.Column<int>(nullable: false),
                    nTime = table.Column<int>(nullable: false),
                    nStartFs = table.Column<int>(nullable: false),
                    nMaxFs = table.Column<int>(nullable: false),
                    nMinFs = table.Column<int>(nullable: false),
                    nLastFs = table.Column<int>(nullable: false),
                    nTotalVolume = table.Column<int>(nullable: false),
                    fVolumeRatio = table.Column<double>(nullable: false),
                    nCount = table.Column<int>(nullable: false),
                    fTotalPrice = table.Column<double>(nullable: false),
                    fBuyPrice = table.Column<double>(nullable: false),
                    fSellPrice = table.Column<double>(nullable: false),
                    fAccumUpPower = table.Column<double>(nullable: false),
                    fAccumDownPower = table.Column<double>(nullable: false),
                    fUpDownPer = table.Column<double>(nullable: false),
                    fInitAngle = table.Column<double>(nullable: false),
                    fMaxAngle = table.Column<double>(nullable: false),
                    fMedianAngle = table.Column<double>(nullable: false),
                    fHourAngle = table.Column<double>(nullable: false),
                    fRecentAngle = table.Column<double>(nullable: false),
                    fDAngle = table.Column<double>(nullable: false),
                    fOverMa0 = table.Column<double>(nullable: false),
                    fOverMa1 = table.Column<double>(nullable: false),
                    fOverMa2 = table.Column<double>(nullable: false),
                    fOverMa0Gap = table.Column<double>(nullable: false),
                    fOverMa1Gap = table.Column<double>(nullable: false),
                    fOverMa2Gap = table.Column<double>(nullable: false),
                    nDownTimeOverMa0 = table.Column<int>(nullable: false),
                    nDownTimeOverMa1 = table.Column<int>(nullable: false),
                    nDownTimeOverMa2 = table.Column<int>(nullable: false),
                    nUpTimeOverMa0 = table.Column<int>(nullable: false),
                    nUpTimeOverMa1 = table.Column<int>(nullable: false),
                    nUpTimeOverMa2 = table.Column<int>(nullable: false),
                    nSummationRanking = table.Column<int>(nullable: false),
                    nSummationMove = table.Column<int>(nullable: false),
                    nMinuteRanking = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_minutehistories", x => new { x.compLoc, x.sDate, x.sCode, x.sCodeName, x.nIdx });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "minutehistories");
        }
    }
}
