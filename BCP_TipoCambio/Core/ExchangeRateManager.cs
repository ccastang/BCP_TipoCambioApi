using AutoMapper;
using BCP_TipoCambio.DTOs;
using BCP_TipoCambio.DTOs.Request;
using BCP_TipoCambio.DTOs.Response;
using BCP_TipoCambio.Entities;
using BCP_TipoCambio.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP_TipoCambio.Core
{
    public class ExchangeRateManager : IExchangeRateManager
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ExchangeRateManager(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ExchangeRateManager()
        {
        }

        public async Task<ResultHelper<ExchangeRateDTO>> Create(AddExchangeRateDTO addExchangeRateDTO)
        {
            var resultado = new ResultHelper<ExchangeRateDTO>();
            try
            {
                if (addExchangeRateDTO.OriginCurrencyId == addExchangeRateDTO.DestinationCurrencyId)
                {
                    resultado.AddError("Los tipos de monedas son iguales,ingrese un tipo de moneda diferente");
                }
                var OriginCurrencyExists = await _context.Money.AnyAsync(x => x.Id == addExchangeRateDTO.OriginCurrencyId);
                var DestinationCurrencyExists = await _context.Money.AnyAsync(x => x.Id == addExchangeRateDTO.DestinationCurrencyId);
                if (!OriginCurrencyExists || !DestinationCurrencyExists)
                {
                    resultado.AddError("Tipo de moneda inexistente");
                }
                var entidad = _mapper.Map<ExchangeRate>(addExchangeRateDTO);
                _context.Add(entidad);
                await _context.SaveChangesAsync();
                var exchangeRateDTO = _mapper.Map<ExchangeRateDTO>(entidad);
                resultado.Value = exchangeRateDTO;
            }
            catch (Exception e)
            {
                resultado.AddError(e.Message);
            }
            return resultado;
        }

        public async Task<ResultHelper<IEnumerable<GetExchangeRateDTO>>> Get()
        {
            List<GetExchangeRateDTO> lista = new List<GetExchangeRateDTO>();
            var resultado = new ResultHelper<IEnumerable<GetExchangeRateDTO>>();
            var entidades = await _context.ExchangeRates.ToListAsync();
            if (entidades.Count > 0)
            {
                foreach (var ent in entidades)
                {
                    var OriginCurrency = _context.Money
                                                 .Where(s => s.Id == ent.OriginCurrencyId)
                                                 .FirstOrDefault();

                    var DestinationCurrency = _context.Money
                                                  .Where(s => s.Id == ent.DestinationCurrencyId)
                                                  .FirstOrDefault();

                    var ExchangeRate = new GetExchangeRateDTO
                    {
                        Id = ent.Id,
                        Buy = ent.Buy,
                        Sale = ent.Sale,
                        OriginCurrencyName = OriginCurrency.Name,
                        DestinationCurrencyName = DestinationCurrency.Name,
                    };
                    lista.Add(ExchangeRate);
                }
                var dtos = _mapper.Map<List<GetExchangeRateDTO>>(lista);
                resultado.Value = dtos;
            }
            else
            {
                resultado.AddError("No existen tipos de cambio en este momento");
            }
            return resultado;
        }

        public async Task<ResultHelper<GetExchangeRateDTO>> GetById(int id)
        {
            var resultado = new ResultHelper<GetExchangeRateDTO>();
            var existe = await _context.ExchangeRates.AnyAsync(x => x.Id == id);
            if (existe)
            {
                var entidad = await _context.ExchangeRates.FirstOrDefaultAsync(x => x.Id == id);
                var OriginCurrency = _context.Money
                                             .Where(s => s.Id == entidad.OriginCurrencyId)
                                             .FirstOrDefault();

                var DestinationCurrency = _context.Money
                                                 .Where(s => s.Id == entidad.DestinationCurrencyId)
                                                 .FirstOrDefault();

                var ExchangeRate = new GetExchangeRateDTO
                {
                    Id = entidad.Id,
                    Buy = entidad.Buy,
                    Sale = entidad.Sale,
                    OriginCurrencyName = OriginCurrency.Name,
                    DestinationCurrencyName = DestinationCurrency.Name,
                };
                var dto = _mapper.Map<GetExchangeRateDTO>(ExchangeRate);
                resultado.Value = dto;
            }
            else
            {
                resultado.AddError("El tipo de cambio no existe");
            }
            return resultado;
        }

        public async Task<ResultHelper<ExchangeRateDTO>> Update(int id,AddExchangeRateDTO addExchangeRateDTO)
        {
            var resultado = new ResultHelper<ExchangeRateDTO>();
            try
            {
                if (addExchangeRateDTO.OriginCurrencyId == addExchangeRateDTO.DestinationCurrencyId)
                {
                    resultado.AddError("Los tipos de monedas son iguales,ingrese un tipo de moneda diferente");
                }
                var OriginCurrencyExists = await _context.Money.AnyAsync(x => x.Id == addExchangeRateDTO.OriginCurrencyId);
                var DestinationCurrencyExists = await _context.Money.AnyAsync(x => x.Id == addExchangeRateDTO.DestinationCurrencyId);
                if (!OriginCurrencyExists || !DestinationCurrencyExists)
                {
                    resultado.AddError("Tipo de moneda inexistente");
                }
                var entidad = _mapper.Map<ExchangeRate>(addExchangeRateDTO);
                entidad.Id = id;
                _context.Entry(entidad).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                resultado.AddError(e.Message);
            }
            return resultado;
        }
    }
}
