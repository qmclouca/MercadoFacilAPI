using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace MercadoFacilAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShareController: ControllerBase
    {
        private readonly IShareService _shareService;

        public ShareController(IShareService shareService)
        {
            _shareService = shareService;
        }

        [HttpGet(Name = "GetAllShares")]
        public async Task<IActionResult> GetAll()
        {
            var shares = await _shareService.GetAllAsync();
            if (shares == null)
                return NotFound();
            return Ok(shares);
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
