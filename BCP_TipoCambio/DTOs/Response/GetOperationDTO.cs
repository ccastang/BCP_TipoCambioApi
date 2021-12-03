using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP_TipoCambio.DTOs.Response
{
    public class GetOperationDTO
    {
        public string Id { get; set; }
        public double Amount { get; set; }
        public double ConvertedAmount { get; set; }
        public double ExchangeRateValue { get; set; }
        public string OriginCurrencyName { get; set; }
        public string DestinationCurrencyName { get; set; }
    }
}
