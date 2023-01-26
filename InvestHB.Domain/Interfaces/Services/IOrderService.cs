using FluentValidation.Results;
using InvestHB.Domain.Commands;
using InvestHB.Domain.Models;

namespace InvestHB.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Tuple<ValidationResult, int>> Create(OrderRequest request);
        Task<Tuple<ValidationResult, int>> Update(OrderUpdateRequest request);
        Task<Tuple<ValidationResult, DeleteOrderStatus>> Delete(DeleteOrderRequest request);
        Task<List<string>> AsCSV(OrderRequest request);
    }
}
