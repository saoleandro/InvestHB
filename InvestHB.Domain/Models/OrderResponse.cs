
using MediatR;

namespace InvestHB.Domain.Models
{
    public class OrderResponse : IRequest
    {
        public int OrderId { get; set; }
    }
}
