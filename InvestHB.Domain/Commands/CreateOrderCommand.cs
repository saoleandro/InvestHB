using FluentValidation.Results;
using InvestHB.Domain.Commands.Validations;
using InvestHB.Domain.Models;

namespace InvestHB.Domain.Commands
{
    public class CreateOrderCommand : OrderCommand
    {
        public CreateOrderCommand(
            int userId, 
            decimal quantity, 
            OrderSide side, 
            OrderType type, 
            decimal price, 
            decimal triggerPrice, 
            string symbol,
            DateTime? expiresAt)
        {
            UserId = userId;
            Quantity = quantity;
            Side = side;
            Type = type;
            Price = price;
            TriggerPrice = triggerPrice;
            Symbol = symbol;
            ExpiresAt = expiresAt;
        }

        public string Symbol { get; set; }
        public DateTime? ExpiresAt { get; set; }

        public ValidationResult Validate()
        {
            var result = new CreateOrderCommandValidation().Validate(this);
            return result;
        }
    }
}
