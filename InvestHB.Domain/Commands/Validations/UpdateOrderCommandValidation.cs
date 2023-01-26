using FluentValidation;

namespace InvestHB.Domain.Commands.Validations
{
    public class UpdateOrderCommandValidation : OrderValidation<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidation()
        {
            ValidateUserId();
            ValidatePrice();
            ValidateQuantity();
            ValidateOrderType();
        }
    }
}
