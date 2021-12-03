using AutoMapper;
using BCP_TipoCambio.Core;
using BCP_TipoCambio.DTOs;
using BCP_TipoCambio.DTOs.Request;
using BCP_TipoCambio.Entities;
using BCP_TipoCambio.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP_TipoCambio.Controllers
{
    [ApiController]
    [Route("api/money")]
    public class MoneyController : ControllerBase
    {
        private readonly IMoneyManager _moneyManager;

        public MoneyController(IMoneyManager moneyManager)
        {
            _moneyManager = moneyManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<MoneyDTO>>> Get()
        {
            var entidades = await _moneyManager.Get();
            if(entidades.Success)
            {
                return Ok(entidades.Value);
            }
            return NotFound(entidades.Errors);
        }

        [HttpGet("{id:int}", Name = "getMoney")]
        public async Task<ActionResult<MoneyDTO>> Get(int id)
        {
            var result = await _moneyManager.GetById(id);
            if(result.Success)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Errors);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddMoneyDTO addMoneyDTO)
        {
            var result = await _moneyManager.Create(addMoneyDTO);
            if(result.Success)
            {
                return new CreatedAtRouteResult("getMoney", new { id = result.Value.Id }, result.Value);
            }
            return BadRequest(result.Errors);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AddMoneyDTO addMoneyDTO)
        {
            var result = await _moneyManager.Update(id,addMoneyDTO);
            if (result.Success)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _moneyManager.Delete(id);
            if(result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
    }
}
