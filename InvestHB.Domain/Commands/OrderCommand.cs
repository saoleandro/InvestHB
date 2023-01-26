using FluentValidation.Results;
using InvestHB.Domain.Models;
using MediatR;

namespace InvestHB.Domain.Commands
{
    public abstract class OrderCommand : IRequest<Tuple<ValidationResult, int>>
    {
        public int UserId { get; set; }
        public decimal Quantity { get; set; }
        public OrderSide Side { get; set; }
        public OrderType Type { get; set; }
        public decimal Price { get; set; }
        public decimal TriggerPrice { get; set; }
    }
}
