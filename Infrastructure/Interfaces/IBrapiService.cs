using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IBrapiService
    {
        public Task<dynamic> GetCompanyQuote(string symbol);
        public Task<dynamic> GetCompanyQuoteHistory(string symbol, int months);
    }
}
