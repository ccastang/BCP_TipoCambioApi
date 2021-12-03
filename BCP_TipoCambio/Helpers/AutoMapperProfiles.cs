using AutoMapper;
using BCP_TipoCambio.DTOs;
using BCP_TipoCambio.DTOs.Request;
using BCP_TipoCambio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP_TipoCambio.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Money, MoneyDTO>().ReverseMap();
            CreateMap<AddMoneyDTO, Money>();
            CreateMap<ExchangeRate, ExchangeRateDTO>().ReverseMap();
            CreateMap<AddExchangeRateDTO, ExchangeRate>();
            CreateMap<Operation, OperationDTO>().ReverseMap();
            CreateMap<AddOperationDTO, Operation>();
        }

        
    }
}
