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
    public class MoneyManager : IMoneyManager
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MoneyManager(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

            if (_context.Money.Count() == 0)
            {
                List<Money> money = new List<Money>();
                money.Add(new Money { Id = 1, Name = "SOLES" });
                money.Add(new Money { Id = 2, Name = "DOLARES" });
                money.Add(new Money { Id = 3, Name = "EUROS" });
                money.Add(new Money { Id = 4, Name = "YENES" });
                _context.Money.AddRange(money);
                _context.SaveChangesAsync();
            }
        }

        public async Task<ResultHelper<IEnumerable<MoneyDTO>>> Get()
        {
            var resultado = new ResultHelper<IEnumerable<MoneyDTO>>();       
            var entidades = await _context.Money.ToListAsync();
            if(entidades.Count >0)
            {
                var dtos = _mapper.Map<List<MoneyDTO>>(entidades);
                resultado.Value = dtos;
            }
            else
            {
                resultado.AddError("No existen monedas en este momento");
            }
            return resultado;
        }

        public async Task<ResultHelper<string>> Delete(int id)
        {
            var resultado = new ResultHelper<string>();
            var existe = await _context.Money.AnyAsync(x => x.Id == id);
            if (existe)
            {
                try
                {
                    _context.Remove(new Money() { Id = id });
                    await _context.SaveChangesAsync();
                    resultado.Value = "La moneda se eliminó correctamente";
                }
                catch (Exception ex )
                {
                    resultado.AddError(ex.Message);
                }
            }        
            else
            {
                resultado.AddError("La moneda no existe");
            }
            return resultado;
        }

        public async Task<ResultHelper<MoneyDTO>> GetById(int id)
        {
            var resultado = new ResultHelper<MoneyDTO>();
            var entidad = await _context.Money.FirstOrDefaultAsync(s => s.Id == id);
            if (entidad != null)
            {
                var dto = _mapper.Map<MoneyDTO>(entidad);
                resultado.Value = dto;
            }
            else
            {
                resultado.AddError("La moneda no existe");
            }
            return resultado;
        }
        public async Task<ResultHelper<MoneyDTO>> Create(AddMoneyDTO addMoneyDTO)
        {
            var resultado = new ResultHelper<MoneyDTO>();
            try
            {
                var entidad = _mapper.Map<Money>(addMoneyDTO);
                _context.Add(entidad);
                await _context.SaveChangesAsync();
                var moneyDTO = _mapper.Map<MoneyDTO>(entidad);
                resultado.Value = moneyDTO;
            }
            catch (Exception e)
            {
                resultado.AddError(e.Message);
            }
            return resultado;
        }

        public async Task<ResultHelper<MoneyDTO>> Update(int id, AddMoneyDTO addMoneyDTO)
        {
            var resultado = new ResultHelper<MoneyDTO>();
            try
            {
                var existe = await _context.Money.AnyAsync(x => x.Id == id);
                if (existe)
                { 
                    var entidad = _mapper.Map<Money>(addMoneyDTO);
                    entidad.Id = id;
                    _context.Entry(entidad).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    resultado.AddError("Estás intentando actualizar una moneda que no existe");
                }              
            }
            catch (Exception e)
            {
                resultado.AddError(e.Message);
            }
            return resultado;
        }
    }
}
