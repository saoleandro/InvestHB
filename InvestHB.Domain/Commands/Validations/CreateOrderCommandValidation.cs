using FluentValidation;
using InvestHB.Domain.Models;

namespace InvestHB.Domain.Commands.Validations
{
    public class CreateOrderCommandValidation : OrderValidation<CreateOrderCommand>
    {
        private readonly OrderType[] _ordersThatShouldHaveExpireDate = new OrderType[] {
            OrderType.Limit,
            OrderType.Stop
        };

        public CreateOrderCommandValidation()
        {
            ValidateUserId();
            ValidatePrice();
            ValidateQuantity();
            ValidateSymbol();
            ValidateExpiresAt();
            ValidateExpiresAtNow();
            ValidateOrderType();
        }

        protected void ValidateSymbol()
        {
            RuleFor(c => c.Symbol)
                .NotEmpty().WithMessage("O instrumento deve conter valor.");
        }

        protected void ValidateExpiresAt()
        {
            RuleFor(c => new { c.ExpiresAt, c.Type })
                .Must(m => ValidExpiresAtOrderType(m.ExpiresAt, m.Type))
                .WithMessage("Para o tipo de ordem especificado a data de validade deve ser preenchida.");
        }

        protected void ValidateExpiresAtNow()
        {
            RuleFor(c => new { c.ExpiresAt, c.Type })
                .Must(m => ValidExpiresNowAtOrderType(m.ExpiresAt, m.Type))
                .WithMessage("Data de expiração inválida.");
        }

        private bool ValidExpiresAtOrderType(DateTime? expiresAt, OrderType type)
        {
            return expiresAt.HasValue && _ordersThatShouldHaveExpireDate.Contains(type);
        }

        private bool ValidExpiresNowAtOrderType(DateTime? expiresAt, OrderType type)
        {
            return expiresAt.HasValue && expiresAt < DateTime.Now && _ordersThatShouldHaveExpireDate.Contains(type);
        }
    }
}
