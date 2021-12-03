using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP_TipoCambio.DTOs.Response
{
    public class GetExchangeRateDTO
    {
        public int Id { get; set; }
        public double Buy { get; set; }
        public double Sale { get; set; }
        public string OriginCurrencyName { get; set; }
        public string DestinationCurrencyName { get; set; }
    }
}
