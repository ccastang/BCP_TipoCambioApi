using BCP_TipoCambio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP_TipoCambio.DTOs
{
    public class ExchangeRateDTO
    {
        public int Id { get; set; }
        public double Buy { get; set; }
        public double Sale { get; set; }
        public int OriginCurrencyId { get; set; }
        public int DestinationCurrencyId { get; set; }
    }
}
