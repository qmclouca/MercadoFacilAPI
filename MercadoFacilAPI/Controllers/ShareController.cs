using Domain.Entities;
using Domain.Interfaces.Services;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MercadoFacilAPI.Controllers
{
    [ApiController]
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

        [HttpGet("{id}", Name = "GetShareById")]
        public async Task<IActionResult> GetShareById(Guid id)
        {
            var share = await _shareService.GetByIdAsync(id);
            if (share == null)
                return NotFound();
            return Ok(share);
        }

        //[HttpGet("{symbol}", Name = "GetShareBySymbol")]
        //public async Task<IActionResult> GetShareBySymbol(string symbol)
        //{
        //    var share = await _shareService.GetBySymbol(symbol);
        //    if (share == null)
        //        return NotFound();
        //    return Ok(share);
        //}
    }
}
