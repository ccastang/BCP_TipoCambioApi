using BCP_TipoCambio.DTOs;
using BCP_TipoCambio.DTOs.Request;
using BCP_TipoCambio.Entities;
using BCP_TipoCambio.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP_TipoCambio.Core
{
    public interface IMoneyManager
    {
        
        Task<ResultHelper<IEnumerable<MoneyDTO>>> Get();
        Task<ResultHelper<MoneyDTO>> GetById(int id);
        Task<ResultHelper<MoneyDTO>> Create(AddMoneyDTO addMoneyDTO);
        Task<ResultHelper<MoneyDTO>> Update(int id,AddMoneyDTO addMoneyDTO);
        Task<ResultHelper<string>> Delete(int id);
    }
}
