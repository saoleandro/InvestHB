﻿using InvestHB.Domain.Interfaces.Repository;
using InvestHB.Domain.Models;
using InvestHB.Repository.InMemory;

namespace InvestHB.Repository
{
    public class InstrumentInfoRepository : IInstrumentInfoRepository
    {
        public InstrumentInfoRepository()
        {
        }
        
        public async Task<InstrumentInfo?> Get(string symbol)
        {
            var instrumentInfo = InMemoryIntrumentInfoRepository.Get(symbol);
            if (instrumentInfo == null)
            {
                Func<InstrumentInfo> mockedData = () => {
                    return new InstrumentInfo
                    {
                        Description = "PETROBRAS",
                        Exchange = "BOVESPA",
                        ISIN = "ABCDE12345",
                        LotStep = 100,
                        MaxLot = 1000000,
                        MinLot = 100,
                        Symbol = "PETR4",
                        Type = InstrumentType.Stock
                    };
                };

                instrumentInfo = await Task.Run(mockedData);

                if (instrumentInfo != null)
                {
                    InMemoryIntrumentInfoRepository.Add(instrumentInfo);
                }
            }

            return instrumentInfo;
        }
    }
}
