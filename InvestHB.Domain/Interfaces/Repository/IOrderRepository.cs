using InvestHB.Domain.Models;

namespace InvestHB.Domain.Interfaces.Repository
{
    public interface IOrderRepository
    {
        Task<int> Add(Order order);
        Task<List<Order>> Get(int userId);
        Task<bool> Delete(int orderId, int userId);
    }
}
