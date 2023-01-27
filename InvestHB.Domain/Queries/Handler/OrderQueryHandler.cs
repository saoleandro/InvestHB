using InvestHB.Domain.Interfaces.Repository;
using InvestHB.Domain.Models;
using MediatR;

namespace InvestHB.Domain.Queries.Handler
{
    public class OrderQueryHandler : IRequestHandler<GetOrderQuery, List<Order>>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository= orderRepository;
        }

        public async Task<List<Order>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.Get(request.UserId);
        }
    }
}
