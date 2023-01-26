using InvestHB.Domain.Models;

namespace InvestHB.Domain.Interfaces.Repository
{
    public  interface IInstrumentInfoRepository
    {
        Task<InstrumentInfo?> Get(string symbol);
    }
}
