using AutoMapper;
using BCP_TipoCambio.Core;
using BCP_TipoCambio.DTOs;
using BCP_TipoCambio.DTOs.Request;
using BCP_TipoCambio.DTOs.Response;
using BCP_TipoCambio.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP_TipoCambio.Controllers
{
    [ApiController]
    [Route("api/exchangerate")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateManager _exchangeRateManager;

        public ExchangeRateController(IExchangeRateManager exchangeRateManager)
        {
            _exchangeRateManager = exchangeRateManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetExchangeRateDTO>>> Get()
        {
            var entidades = await _exchangeRateManager.Get();
            if (entidades.Success)
            {
                return Ok(entidades.Value);
            }
            return NotFound(entidades.Errors);
        }

        [HttpGet("{id:int}", Name = "getExchangeRate")]
        public async Task<ActionResult<GetExchangeRateDTO>> Get(int id)
        {
            var result = await _exchangeRateManager.GetById(id);
            if (result.Success)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Errors);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> Post(int id, [FromBody] AddExchangeRateDTO addExchangeRateDTO)
        {
            var result = await _exchangeRateManager.Update(id, addExchangeRateDTO);
            if (result.Success)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddExchangeRateDTO addExchangeRateDTO)
        {
            var result = await _exchangeRateManager.Create(addExchangeRateDTO);
            if (result.Success)
            {
                return new CreatedAtRouteResult("getExchangeRate", new { id = result.Value.Id }, result.Value);
            }
            return BadRequest(result.Errors);
        }
    }
}
