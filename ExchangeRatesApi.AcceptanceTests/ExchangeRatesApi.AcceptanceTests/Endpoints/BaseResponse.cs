using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesApi.AcceptanceTests.Endpoints
{
    public class BaseResponse<T> where T : BaseSuccessfulResponse
    {
        public IRestResponse OriginalResponse { get; set; }

        public T SuccessfulResponse { get; set; }

        public Error Error { get; set; }
    }
}
