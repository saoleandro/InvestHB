using InvestHB.Domain.Models;

namespace InvestHB.Domain.Interfaces.Services
{
    public interface IInstrumentInfoService
    {
        Task<InstrumentInfo?> GetBySymbol(string symbol);
    }
}
