namespace Domain.Entities.ReturnObjects
{
    public class EarningsByShareSymbol
    {
        public EarningsByShareSymbol()
        {
        }
        public EarningsByShareSymbol(string symbol, string companyName, double earningsPerShare)
        {
            this.symbol = symbol;
            this.companyName = companyName;            
            this.earningsPerShare = earningsPerShare;            
        }
        public string? symbol { get; set; }
        public string? companyName { get; set; }
        public double? earningsPerShare { get; set; }
    }
}
