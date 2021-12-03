using BCP_TipoCambio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP_TipoCambio.DTOs.Request
{
    public class AddExchangeRateDTO
    {
        public int OriginCurrencyId { get; set; }
        public int DestinationCurrencyId { get; set; }
        public double Buy { get; set; }
        public double Sale { get; set; }
    }
}
