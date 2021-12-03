using AutoMapper;
using BCP_TipoCambio.DTOs;
using BCP_TipoCambio.DTOs.Request;
using BCP_TipoCambio.Entities;
using BCP_TipoCambio.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP_TipoCambio.Core
{
    public class OperationManager : IOperationManager
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public OperationManager(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public string GetCurrencyName(int CurrencyId)
        {
            var Currency =  _context.Money.Where(s => s.Id == CurrencyId)
                                               .FirstOrDefault();
            return Currency.Name;
        }

        public Operation Operation(ExchangeRate exchangeRate, AddOperationDTO addOperationDTO,bool isSale)
        {
            var Operation = new OperationDTO
            {
                OriginCurrencyName = isSale ? GetCurrencyName(exchangeRate.OriginCurrencyId): GetCurrencyName(exchangeRate.DestinationCurrencyId),
                DestinationCurrencyName = isSale ? GetCurrencyName(exchangeRate.DestinationCurrencyId): GetCurrencyName(exchangeRate.OriginCurrencyId),
                Amount = addOperationDTO.Amount,
                ExchangeRateValue = isSale? exchangeRate.Sale : exchangeRate.Buy,
                ConvertedAmount = isSale ? (addOperationDTO.Amount / exchangeRate.Sale) : addOperationDTO.Amount * exchangeRate.Buy
            };

            var entidad = _mapper.Map<Operation>(Operation);
            return entidad;
        }

        public ExchangeRate GetExchangeRate(AddOperationDTO addOperationDTO)
        {
            var ExchangeRate = _context.ExchangeRates.Where(x => x.OriginCurrencyId == addOperationDTO.OriginCurrencyId
                                                        &&
                                                        x.DestinationCurrencyId == addOperationDTO.DestinationCurrencyId
                                                        )
                                                     .FirstOrDefault();
            return ExchangeRate;
        }
        public async Task<ResultHelper<OperationDTO>> Create(AddOperationDTO addOperationDTO)
        {
            var resultado = new ResultHelper<OperationDTO>();
            try
            {
                int monedaOrigen = 0;
                if (addOperationDTO.OriginCurrencyId == addOperationDTO.DestinationCurrencyId)
                {
                    resultado.AddError("Los tipos de monedas son iguales,ingrese un tipo de moneda diferente");
                }
                var ExchangeRate = GetExchangeRate(addOperationDTO);

                monedaOrigen = (ExchangeRate != null) ? ExchangeRate.OriginCurrencyId : 0;

                if (monedaOrigen == addOperationDTO.OriginCurrencyId) //Sale
                {
                    var operation = Operation(ExchangeRate, addOperationDTO, true);
                    _context.Add(operation);
                    await _context.SaveChangesAsync();
                    var operationDTO = _mapper.Map<OperationDTO>(operation);
                    resultado.Value = operationDTO;
                }
                else //Buy
                {
                    var ExchangeRate2 = _context.ExchangeRates
                                                 .Where(x => x.DestinationCurrencyId == addOperationDTO.OriginCurrencyId
                                                        )
                                                 .FirstOrDefault();
                    if (ExchangeRate2.OriginCurrencyId != addOperationDTO.DestinationCurrencyId)
                    {
                        resultado.AddError("No existe el tipo de cambio");
                    }

                    var operation = Operation(ExchangeRate2, addOperationDTO, false);
                    _context.Add(operation);
                    await _context.SaveChangesAsync();
                    var operationDTO = _mapper.Map<OperationDTO>(operation);
                    resultado.Value = operationDTO;
                }
            }
            catch (Exception e)
            {
                resultado.AddError(e.Message);
            }
            return resultado;         
        }

        public async Task<ResultHelper<OperationDTO>> GetById(int id)
        {
            var resultado = new ResultHelper<OperationDTO>();
            var entidad = await _context.Operations.FirstOrDefaultAsync(s => s.Id == id);
            if (entidad != null)
            {
                var dto = _mapper.Map<OperationDTO>(entidad);
                resultado.Value = dto;
            }
            else
            {
                resultado.AddError("La operación no existe");
            }
            return resultado;
        }

        public async Task<ResultHelper<IEnumerable<OperationDTO>>> Get()
        {
            var resultado = new ResultHelper<IEnumerable<OperationDTO>>();
            var entidades = await _context.Operations.ToListAsync();
            if (entidades.Count > 0)
            {
                var dtos = _mapper.Map<List<OperationDTO>>(entidades);
                resultado.Value = dtos;
            }
            else
            {
                resultado.AddError("No existen operaciones en este momento");
            }
            return resultado;
        }
    }
}
