using AutoMapper;
using FluentValidation.Results;
using InvestHB.Domain.Commands;
using InvestHB.Domain.Interfaces.Services;
using InvestHB.Domain.Models;
using InvestHB.Domain.Queries;
using MediatR;

namespace InvestHB.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public OrderService(IMapper mapper,
                            IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public Task<List<string>> AsCSV(OrderRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Order>> GetOrders(int userId)
        {
            return await _mediator.Send(new GetOrderQuery(userId));
        }

        public async Task<Tuple<ValidationResult, int>> Create(OrderRequest request)
        {
            var createOrderCommand = _mapper.Map<CreateOrderCommand>(request);
            return await _mediator.Send(createOrderCommand);
        }

        public async Task<Tuple<ValidationResult, DeleteOrderStatus>> Delete(DeleteOrderRequest request)
        {
            var deleteOrderCommand = _mapper.Map<DeleteOrderCommand>(request);
            Tuple<ValidationResult, int> result = await _mediator.Send(deleteOrderCommand);

             return new Tuple<ValidationResult, DeleteOrderStatus>(new ValidationResult(), result.Item2 == 0 ? DeleteOrderStatus.Success : DeleteOrderStatus.RejectedByExchange);
        }

        public async Task<Tuple<ValidationResult, int>> Update(OrderUpdateRequest request)
        {
            var updateOrderCommand = _mapper.Map<UpdateOrderCommand>(request);
            return await _mediator.Send(updateOrderCommand);
        }
    }
}
