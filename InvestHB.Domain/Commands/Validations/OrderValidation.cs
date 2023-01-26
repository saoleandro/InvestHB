using FluentValidation;
using InvestHB.Domain.Interfaces.Repository;
using InvestHB.Domain.Models;

namespace InvestHB.Domain.Commands.Validations
{
    public abstract class OrderValidation<T> : AbstractValidator<T> where T : OrderCommand
    {
        protected void ValidateUserId()
        {
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("Favor informar o usuário")
                .GreaterThan(0).WithMessage("Usuário inválido");
        }

        protected void ValidatePrice()
        {
            RuleFor(c => c.Price)
                .NotEmpty().WithMessage("Favor informar o preço")
                .GreaterThan(0).WithMessage("O preço não pode ser menor do que zero.");
        }

        protected void ValidateQuantity()
        {
            RuleFor(c => c.Quantity)
                .NotEmpty().WithMessage("Favor informar a quantidade")
                .GreaterThan(0).WithMessage("A quantidade não pode ser menor do que zero.");
        }

        protected void ValidateOrderType()
        {
            RuleFor(c => c)
                .Must(m => CheckTriggerPrice(m.Type, m.TriggerPrice))
                .WithMessage("O preço de gatilho deve ser preenchido quando a ordem é de stop.");
        }

        private bool CheckTriggerPrice(OrderType type, decimal triggerPrice)
        {
            if (type == OrderType.Stop && triggerPrice <= 0)
                return false;

            return true;
        }
    }
}
