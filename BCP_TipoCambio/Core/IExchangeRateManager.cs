using BCP_TipoCambio.DTOs;
using BCP_TipoCambio.DTOs.Request;
using BCP_TipoCambio.DTOs.Response;
using BCP_TipoCambio.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP_TipoCambio.Core
{
    public interface IExchangeRateManager
    {
        Task<ResultHelper<IEnumerable<GetExchangeRateDTO>>> Get();
        Task<ResultHelper<GetExchangeRateDTO>> GetById(int id);
        Task<ResultHelper<ExchangeRateDTO>> Create(AddExchangeRateDTO addExchangeRateDTO);
        Task<ResultHelper<ExchangeRateDTO>> Update(int id,AddExchangeRateDTO addExchangeRateDTO);
    }
}
