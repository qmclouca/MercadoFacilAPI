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
                    TwoHundredDayAverage = table.Column<double>(type: "float", nullable: false),
                    TwoHundredDayAverageChange = table.Column<double>(type: "float", nullable: false),
                    TwoHundredDayAverageChangePercent = table.Column<double>(type: "float", nullable: false),
                    MarketCap = table.Column<long>(type: "bigint", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LongName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegularMarketChange = table.Column<double>(type: "float", nullable: false),
                    RegularMarketChangePercent = table.Column<double>(type: "float", nullable: false),
                    RegularMarketTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegularMarketPrice = table.Column<double>(type: "float", nullable: false),
                    RegularMarketDayHigh = table.Column<double>(type: "float", nullable: false),
                    RegularMarketDayRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegularMarketDayLow = table.Column<double>(type: "float", nullable: false),
                    RegularMarketVolume = table.Column<long>(type: "bigint", nullable: false),
                    RegularMarketPreviousClose = table.Column<double>(type: "float", nullable: false),
                    RegularMarketOpen = table.Column<double>(type: "float", nullable: false),
                    AverageDailyVolume3Month = table.Column<long>(type: "bigint", nullable: false),
                    AverageDailyVolume10Day = table.Column<long>(type: "bigint", nullable: false),
                    FiftyTwoWeekLowChange = table.Column<double>(type: "float", nullable: false),
                    FiftyTwoWeekLowChangePercent = table.Column<double>(type: "float", nullable: false),
                    FiftyTwoWeekRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FiftyTwoWeekHighChange = table.Column<double>(type: "float", nullable: false),
                    FiftyTwoWeekHighChangePercent = table.Column<double>(type: "float", nullable: false),
                    FiftyTwoWeekLow = table.Column<double>(type: "float", nullable: false),
                    FiftyTwoWeekHigh = table.Column<double>(type: "float", nullable: false),
                    PriceEarnings = table.Column<double>(type: "float", nullable: false),
                    EarningsPerShare = table.Column<double>(type: "float", nullable: false),
                    Logourl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
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
