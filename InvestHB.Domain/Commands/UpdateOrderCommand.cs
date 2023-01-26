using FluentValidation;
using FluentValidation.Results;
using InvestHB.Domain.Commands.Validations;
using InvestHB.Domain.Models;

namespace InvestHB.Domain.Commands
{
    public class UpdateOrderCommand : OrderCommand
    {
        public UpdateOrderCommand(
            int userId, 
            int orderId, 
            string instrument, 
            decimal quantity, 
            OrderSide side, 
            OrderType type, 
            decimal price, 
            decimal triggerPrice)
        {
            UserId = userId;
            OrderId = orderId;
            Instrument = instrument;
            Quantity = quantity;
            Side = side;
            Type = type;
            Price = price;
            TriggerPrice = triggerPrice;
        }

        public int OrderId { get; set; }
        public string Instrument { get; set; }

        public ValidationResult Validate()
        {
            var result = new UpdateOrderCommandValidation().Validate(this);
            return result;
        }
    }
}
