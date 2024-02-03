namespace Infrastructure.Interfaces
{
    public interface IBrapiService
    {
        public Task<dynamic> GetCompanyQuote(string symbol);
        public Task<dynamic> GetCompanyQuoteHistory(string symbol, int months);
        public Task<Task> SaveAllCompanyQuotes();
    }
}
