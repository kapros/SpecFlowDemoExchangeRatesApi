using RestSharp;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExchangeRatesApi.AcceptanceTests.Endpoints
{
    public class EndpointsBase
    {
        protected readonly IRestClient _client;

        protected EndpointsBase(IRestClient client)
        {
            _client = client;
        }

        protected async Task<IRestResponse> Send(IRestRequest request) => await _client.ExecuteAsync(request);

        protected JsonSerializerOptions SerializerOptions
        {
            get
                =>
                new()
                {
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                    IgnoreNullValues = true,
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
                };
        }
    }
}