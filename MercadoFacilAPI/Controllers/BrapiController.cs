using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MercadoFacilAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrapiController : ControllerBase
    {
        private readonly IBrapiService _brapiService;
        public BrapiController(IBrapiService brapiService)
        {
            _brapiService = brapiService;            
        }

        [HttpGet("/GetDataCompany/{symbol}")]
        public async Task<IActionResult> Get(string symbol)
        {
            try
            {
                var result = await _brapiService.GetCompanyQuote(symbol);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
