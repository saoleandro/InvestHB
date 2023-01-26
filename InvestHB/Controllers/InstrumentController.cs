using InvestHB.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestHB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentController : Controller
    {
        private readonly IInstrumentInfoRepository _instrumentInfoRepository;

        public InstrumentController(IInstrumentInfoRepository instrumentInfoRepository)
        {
            _instrumentInfoRepository = instrumentInfoRepository;
        }

        [HttpGet]
        [Route("{symbol}")]
        public async Task<IActionResult> Get([FromRoute] string symbol)
        {
            try
            {
                return Ok(await _instrumentInfoRepository.Get(symbol));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
