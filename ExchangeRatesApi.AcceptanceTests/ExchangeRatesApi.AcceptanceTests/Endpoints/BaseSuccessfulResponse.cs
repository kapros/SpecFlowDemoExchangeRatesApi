using RestSharp;

namespace ExchangeRatesApi.AcceptanceTests.Endpoints
{
    public class BaseSuccessfulResponse
    {
        public bool? Success { get; set; }

        public long? TimeStamp { get; set; }
    }
}
