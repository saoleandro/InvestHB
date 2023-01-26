using InvestHB.Domain.Commands;
using InvestHB.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using InvestHB.Domain.Models;
using System.Runtime.CompilerServices;
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
        private readonly IMediator _mediator;

        public OrdersController(
            IInstrumentInfoRepository instrumentInfoRepository,
            IOrderRepository orderRepository,
            IOrderService orderService,
            IMediator mediator)
        {
            _instrumentInfoRepository = instrumentInfoRepository;
            _orderRepository = orderRepository;
            _orderService = orderService;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] OrderRequest request)
        {
            try
            {
                var instrumentInfo = await _instrumentInfoRepository.Get(request.Symbol);

                if (instrumentInfo != null && (request.Quantity % instrumentInfo.LotStep != 0 ||
                   request.Quantity < instrumentInfo.MinLot ||
                   request.Quantity > instrumentInfo.MaxLot))
                    return BadRequest("Quantidade inválida.");


                var resultValidation = await _orderService.Create(request);

                var errors = Validate(resultValidation.Item1);

                if(errors != null)
                    return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
                    {
                        { "Messages", errors.ToArray() }
                    }));
               

                return Ok(resultValidation.Item2);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteOrderRequest request)
        {
            try
            {
                var resultValidation = await _orderService.Delete(request);
                var errors = Validate(resultValidation.Item1);

                if (errors != null)
                    return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
                    {
                        { "Messages", errors.ToArray() }
                    }));


                return Ok(resultValidation.Item2);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            try
            {
                var orders = await _orderRepository.Get(userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("csv")]
        public async Task<IActionResult> GetAsCSV([FromBody] OrderRequest request)
        {
            try
            {
                return Ok(await _orderService.AsCSV(request));
            }
            catch (Exception ex)
            {
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
