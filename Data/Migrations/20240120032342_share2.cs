using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class share2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shares",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TwoHundredDayAverage = table.Column<double>(type: "float", nullable: true),
                    TwoHundredDayAverageChange = table.Column<double>(type: "float", nullable: true),
                    TwoHundredDayAverageChangePercent = table.Column<double>(type: "float", nullable: true),
                    MarketCap = table.Column<long>(type: "bigint", nullable: true),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LongName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegularMarketChange = table.Column<double>(type: "float", nullable: true),
                    RegularMarketChangePercent = table.Column<double>(type: "float", nullable: true),
                    RegularMarketTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegularMarketPrice = table.Column<double>(type: "float", nullable: true),
                    RegularMarketDayHigh = table.Column<double>(type: "float", nullable: true),
                    RegularMarketDayRange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegularMarketDayLow = table.Column<double>(type: "float", nullable: true),
                    RegularMarketVolume = table.Column<long>(type: "bigint", nullable: true),
                    RegularMarketPreviousClose = table.Column<double>(type: "float", nullable: true),
                    RegularMarketOpen = table.Column<double>(type: "float", nullable: true),
                    AverageDailyVolume3Month = table.Column<long>(type: "bigint", nullable: true),
                    AverageDailyVolume10Day = table.Column<long>(type: "bigint", nullable: true),
                    FiftyTwoWeekLowChange = table.Column<double>(type: "float", nullable: true),
                    FiftyTwoWeekLowChangePercent = table.Column<double>(type: "float", nullable: true),
                    FiftyTwoWeekRange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FiftyTwoWeekHighChange = table.Column<double>(type: "float", nullable: true),
                    FiftyTwoWeekHighChangePercent = table.Column<double>(type: "float", nullable: true),
                    FiftyTwoWeekLow = table.Column<double>(type: "float", nullable: true),
                    FiftyTwoWeekHigh = table.Column<double>(type: "float", nullable: true),
                    PriceEarnings = table.Column<double>(type: "float", nullable: true),
                    EarningsPerShare = table.Column<double>(type: "float", nullable: true),
                    Logourl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shares", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shares");
        }
    }
}
