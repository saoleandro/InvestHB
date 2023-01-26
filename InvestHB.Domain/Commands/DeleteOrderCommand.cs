using InvestHB.Domain.Commands.Validations;

namespace InvestHB.Domain.Commands
{
    public class DeleteOrderCommand : OrderCommand
    {
        public DeleteOrderCommand(
            int userId,
            int orderId)
        {
            UserId = userId;
            OrderId = orderId;
        }

        public int OrderId { get; set; }
    }

    public enum DeleteOrderStatus
    {
        Success = 0,
        RejectedByExchange
    }
}
