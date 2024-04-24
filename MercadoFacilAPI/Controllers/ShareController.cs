using Domain.Entities.ReturnObjects;
using Domain.Interfaces.Services;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MercadoFacilAPI.Controllers
{
    [Authorize]
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class ShareController: ControllerBase
    {
        private readonly IShareService _shareService;
        private readonly IPaginationService _paginationService;

        public ShareController(IShareService shareService, IPaginationService paginationService)
        {
            _shareService = shareService;
            _paginationService = paginationService;
        }

        [HttpGet("{page}, {resultsByPage}", Name = "GetPaginatedShares")]        
        public async Task<IActionResult> GetPaginatedShares(int page, int resultsByPage)
        {
            var sharesEnumerable = await _shareService.GetAllSharesQuery();
            if (sharesEnumerable == null)
                return NotFound();

            var sharesQuery = sharesEnumerable.AsQueryable(); 
            var paginatedShares = await _paginationService.PaginateAsync(sharesQuery, page, resultsByPage);
            return Ok(paginatedShares);
        }

        [HttpGet("{symbol}", Name = "GetShareBySymbol")]        
        public async Task<IActionResult> GetShareBySymbol(string symbol)
        {
            var sharesEnumerable = await _shareService.GetAllSharesQuery();
            if (sharesEnumerable == null)
                return NotFound();

            var share = sharesEnumerable.OrderBy(s => s.RegularMarketTime).Where(s => s.Symbol!.ToUpper().Equals(symbol.ToUpper())).LastOrDefault();
            return Ok(share);
        }
        [HttpGet(Name = "GetListOfEarningsByShare")]        
        public async Task<IActionResult> GetListOfEarningsByShare()
        {      
            var earningsQuery = _shareService
                .GetAllSharesQuery().Result                
                .GroupBy(share => share!.Symbol!.ToUpper())
                .Select(group => new EarningsByShareSymbol
                {
                    symbol = group.Key,
                    companyName = group.OrderByDescending(s => s.CreatedAt).FirstOrDefault()!.LongName,
                    earningsPerShare = group.OrderByDescending(s => s.CreatedAt).FirstOrDefault()!.EarningsPerShare
                })
                .OrderByDescending(e => e.earningsPerShare);

            
            if (!earningsQuery.Any())
                return NotFound();

            return Ok(earningsQuery);
        }
    }
}