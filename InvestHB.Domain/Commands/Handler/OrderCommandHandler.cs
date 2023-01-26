using FluentValidation.Results;
using InvestHB.Domain.Interfaces.Repository;
using InvestHB.Domain.Models;
using MediatR;

namespace InvestHB.Domain.Commands.Handler
{
    public class OrderCommandHandler :
        IRequestHandler<CreateOrderCommand, Tuple<ValidationResult, int>>,
        IRequestHandler<UpdateOrderCommand, Tuple<ValidationResult, int>>,
        IRequestHandler<DeleteOrderCommand, Tuple<ValidationResult, int>>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository= orderRepository;
        }

        public async Task<Tuple<ValidationResult, int>> Handle(CreateOrderCommand message, CancellationToken cancellationToken)
        {
            ValidationResult result = message.Validate();

            if (!result.IsValid)
                return new Tuple<ValidationResult, int>(result,0);

            var order = new Order(
                message.UserId,
                message.Symbol,
                message.Quantity,
                message.Side,
                message.Type,
                OrderStatus.Open,
                message.Price,
                message.TriggerPrice,
                message.ExpiresAt,
                DateTime.Now
                );

            var orderId = await _orderRepository.Add(order);
            return new Tuple<ValidationResult, int>(result, orderId);
        }

        public async Task<Tuple<ValidationResult, int>> Handle(UpdateOrderCommand message, CancellationToken cancellationToken)
        {
            ValidationResult result = message.Validate();

            return await Task.FromResult(new Tuple<ValidationResult, int>(result, message.OrderId));
        }

        public async Task<Tuple<ValidationResult, int>> Handle(DeleteOrderCommand message, CancellationToken cancellationToken)
        {
            var result = await _orderRepository.Delete(message.OrderId, message.UserId);

            return new Tuple<ValidationResult, int>(new ValidationResult(), result ? 0 : 1);
        }
    }
}
