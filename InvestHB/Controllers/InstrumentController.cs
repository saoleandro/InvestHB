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
        private readonly ILogger<InstrumentController> _logger;

        public InstrumentController(IInstrumentInfoRepository instrumentInfoRepository, ILogger<InstrumentController> logger)
        {
            _instrumentInfoRepository = instrumentInfoRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("{symbol}")]
        public async Task<IActionResult> Get([FromRoute] string symbol)
        {
            try
            {
                _logger.LogInformation($"Consultando pelo ativo {symbol}", "InstrumentController");

                return Ok(await _instrumentInfoRepository.Get(symbol));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error: {ex.Message}", "InstrumentController");

                return BadRequest(ex.Message);
            }
        }
    }
}
