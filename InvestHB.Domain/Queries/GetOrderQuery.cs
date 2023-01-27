using InvestHB.Domain.Models;
using MediatR;

namespace InvestHB.Domain.Queries
{
    public class GetOrderQuery : IRequest<List<Order>>
    {
        public GetOrderQuery(int userId)        
        {
            UserId = userId;
        }

        public int UserId { get; set; }

    }
}
