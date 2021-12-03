using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP_TipoCambio.Entities
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public double Buy { get; set; }
        public double Sale { get; set; }
        public int OriginCurrencyId { get; set; }
        public int DestinationCurrencyId { get; set; }

        //public List<Money> Money { get; set; }
        //public Money DestinationCurrency { get; set; }
    }
}
