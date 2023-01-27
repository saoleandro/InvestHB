using InvestHB.Domain.Models;
using MediatR;

namespace InvestHB.Domain.Queries
{
    public class GetInstrumentInfoQuery : IRequest<InstrumentInfo?>
    {
        public GetInstrumentInfoQuery(string symbol)
        {
            Symbol= symbol;
        }

        public string Symbol { get; set; }
    }
}
