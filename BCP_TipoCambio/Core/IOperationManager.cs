using BCP_TipoCambio.DTOs;
using BCP_TipoCambio.DTOs.Request;
using BCP_TipoCambio.Entities;
using BCP_TipoCambio.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP_TipoCambio.Core
{
    public interface IOperationManager
    {
        Task<ResultHelper<OperationDTO>> Create(AddOperationDTO addOperationDTO);
        Task<ResultHelper<OperationDTO>> GetById(int id);
        Task<ResultHelper<IEnumerable<OperationDTO>>> Get();
    }
}
