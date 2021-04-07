using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesApi.AcceptanceTests.Endpoints.Latest
{
    public class LatestEndpointResponse : BaseSuccessfulResponse
    {
        public string Base { get; set; }

        public DateTime? Date { get; set; }

        public Dictionary<string, float> Rates { get; set; }
    }
}
