namespace Domain.Entities
{
    public class Share: BaseEntity
    {       
        public string? Symbol { get; set; }       
        public string? Currency { get; set; }        
        public double? TwoHundredDayAverage { get; set; }       
        public double? TwoHundredDayAverageChange { get; set; }        
        public double? TwoHundredDayAverageChangePercent { get; set; }       
        public long? MarketCap { get; set; }        
        public string? ShortName { get; set; }       
        public string? LongName { get; set; }        
        public double? RegularMarketChange { get; set; }        
        public double? RegularMarketChangePercent { get; set; }        
        public DateTime? RegularMarketTime { get; set; }        
        public double? RegularMarketPrice { get; set; }        
        public double? RegularMarketDayHigh { get; set; }       
        public string? RegularMarketDayRange { get; set; }        
        public double? RegularMarketDayLow { get; set; }       
        public long? RegularMarketVolume { get; set; }       
        public double? RegularMarketPreviousClose { get; set; }       
        public double? RegularMarketOpen { get; set; }        
        public long? AverageDailyVolume3Month { get; set; }        
        public long? AverageDailyVolume10Day { get; set; }        
        public double? FiftyTwoWeekLowChange { get; set; }       
        public double? FiftyTwoWeekLowChangePercent { get; set; }       
        public string? FiftyTwoWeekRange { get; set; }       
        public double? FiftyTwoWeekHighChange { get; set; }        
        public double? FiftyTwoWeekHighChangePercent { get; set; }       
        public double? FiftyTwoWeekLow { get; set; }       
        public double? FiftyTwoWeekHigh { get; set; }       
        public double? PriceEarnings { get; set; }      
        public double? EarningsPerShare { get; set; }     
        public string? Logourl { get; set; }
    }
}