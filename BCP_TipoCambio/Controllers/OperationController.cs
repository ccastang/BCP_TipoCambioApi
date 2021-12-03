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
    [Route("api/operation")]
    public class OperationController : ControllerBase
    {
        private readonly IOperationManager _operationManager;

        public OperationController(IOperationManager operationManager)
        {
            _operationManager = operationManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<MoneyDTO>>> Get()
        {
            var entidades = await _operationManager.Get();
            if (entidades.Success)
            {
                return Ok(entidades.Value);
            }
            return NotFound(entidades.Errors);
        }

        [HttpGet("{id:int}", Name = "getOperation")]
        public async Task<ActionResult<OperationDTO>> Get(int id)
        {
            var result = await _operationManager.GetById(id);
            if (result.Success)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Errors);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddOperationDTO addOperationDTO)
        {
            var result = await _operationManager.Create(addOperationDTO);
            if (result.Success)
            {
                return new CreatedAtRouteResult("getOperation", new { id = result.Value.Id }, result.Value);
            }
            return BadRequest(result.Errors);
        }
    }
}
