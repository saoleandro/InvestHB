using InvestHB.Domain.Interfaces.Repository;
using InvestHB.Domain.Models;
using MediatR;

namespace InvestHB.Domain.Queries.Handler
{
    public class InstrumentInfoQueryHandler : IRequestHandler<GetInstrumentInfoQuery, InstrumentInfo?>
    {
        private IInstrumentInfoRepository _instrumentInfoRepository;

        public InstrumentInfoQueryHandler(IInstrumentInfoRepository instrumentInfoRepository)
        {
            _instrumentInfoRepository = instrumentInfoRepository;
        }

        public async Task<InstrumentInfo?> Handle(GetInstrumentInfoQuery request, CancellationToken cancellationToken)
        {
            return await _instrumentInfoRepository.Get(request.Symbol);
        }
    }
}
