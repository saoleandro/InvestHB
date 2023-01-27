using InvestHB.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using InvestHB.Domain.Models;
using InvestHB.Domain.Interfaces.Services;
using FluentValidation.Results;

namespace InvestHB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IInstrumentInfoRepository _instrumentInfoRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(
            IInstrumentInfoRepository instrumentInfoRepository,
            IOrderRepository orderRepository,
            IOrderService orderService,
            ILogger<OrdersController> logger)
        {
            _instrumentInfoRepository = instrumentInfoRepository;
            _orderRepository = orderRepository;
            _orderService = orderService;
            _logger = logger;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] OrderRequest request)
        {
            try
            {
                _logger.LogInformation($"Enviando ordem de compra. UserId: {request.UserId} | Symbol {request.Symbol}", "OrdersController | Add");

                var instrumentInfo = await _instrumentInfoRepository.Get(request.Symbol);

                if (instrumentInfo != null && (request.Quantity % instrumentInfo.LotStep != 0 ||
                   request.Quantity < instrumentInfo.MinLot ||
                   request.Quantity > instrumentInfo.MaxLot))
                {
                    _logger.LogWarning($"Quantidade inválida para o lote do ativo. Lote Min.: {instrumentInfo.MinLot} - Lote Max.:{instrumentInfo.MaxLot}", "OrdersController | Add");
                    return BadRequest("Quantidade inválida.");
                }


                var resultValidation = await _orderService.Create(request);

                var errors = Validate(resultValidation.Item1);

                if (errors != null)
                {
                    _logger.LogError($"{string.Join(@"|", errors.ToArray())}", "Enviar alocação", "OrdersController | Add");

                    return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
                    {
                        { "Messages", errors.ToArray() }
                    }));
                }
               

                return Ok(resultValidation.Item2);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error: {ex.Message}", "Exception", "OrdersController | Add");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteOrderRequest request)
        {
            try
            {
                _logger.LogInformation($"Enviando solicitação de excluir a ordem. UserId: {request.UserId} | OrderId {request.OrderId}", "OrdersController | delete");

                var resultValidation = await _orderService.Delete(request);
                var errors = Validate(resultValidation.Item1);

                if (errors != null)
                {
                    _logger.LogError($"{string.Join(@"|", errors.ToArray())}", "Enviar alocação", "OrdersController | Delete");

                    return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
                    {
                        { "Messages", errors.ToArray() }
                    }));
                }

                return Ok(resultValidation.Item2);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error: {ex.Message}", "Exception", "OrdersController | Delete");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            try
            {
                _logger.LogInformation($"Enviando solicitação de Consultar orders do UserId: {userId}", "OrdersController | Get");
                var orders = await _orderRepository.Get(userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error: {ex.Message}", "Exception", "OrdersController | Get");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("csv")]
        public async Task<IActionResult> GetAsCSV([FromBody] OrderRequest request)
        {
            try
            {
                _logger.LogInformation($"Enviando solicitação gerar CSV - UserId: {request.UserId} | Symbol: {request.Symbol}", "OrdersController | GetAsCSV");
                return Ok(await _orderService.AsCSV(request));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error: {ex.Message}", "Exception", "OrdersController | GetAsCSV");
                return BadRequest(ex.Message);
            }
        }

        private List<string>? Validate(ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
            {
                List<string> errors = new List<string>();

                foreach (var error in validationResult.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }

                return errors;
            }

            return null;
        }
    }
}
