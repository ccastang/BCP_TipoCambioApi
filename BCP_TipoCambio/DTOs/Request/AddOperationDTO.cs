using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP_TipoCambio.DTOs.Request
{
    public class AddOperationDTO
    {
        public int OriginCurrencyId { get; set; }
        public int DestinationCurrencyId { get; set; }
        public double Amount { get; set; }
    }
}
