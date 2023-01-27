using InvestHB.Domain.Interfaces.Repository;
using InvestHB.Domain.Interfaces.Services;
using InvestHB.Domain.Models;
using InvestHB.Domain.Queries;
using MediatR;

namespace InvestHB.Domain.Services
{
    public class InstrumentInfoService : IInstrumentInfoService
    {

        private readonly IMediator _mediator;
        
        public InstrumentInfoService(IMediator mediator)
        {
            _mediator= mediator;
        }

        public async Task<InstrumentInfo?> GetBySymbol(string symbol)
        {
            return await _mediator.Send(new GetInstrumentInfoQuery(symbol));
        }
    }
}
