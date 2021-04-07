using ExchangeRatesApi.AcceptanceTests.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExchangeRatesApi.AcceptanceTests.Endpoints.Latest
{
    public class Latest : EndpointsBase
    {
        public const string ENDPOINT = "v1/latest";

        public Latest(IRestClient client) : base(client)
        {
        }

        public async Task<BaseResponse<LatestEndpointResponse>> SendGet(string accessKey, IEnumerable<string> currencies)
        {
            var request = new RestRequest(ENDPOINT, Method.GET);
            request.AddApiKey(accessKey);
            if (currencies.Count() > 0)
            {
                request.AddParameter("symbols", string.Join(",", currencies));
            }
            var response = await Send(request);
            var latestEndpointResponse = new BaseResponse<LatestEndpointResponse>
            {
                OriginalResponse = response,
                Error = JsonSerializer.Deserialize<ErrorResponse>(response.Content, SerializerOptions).Error,
                SuccessfulResponse = JsonSerializer.Deserialize<LatestEndpointResponse>(response.Content, SerializerOptions)
            };
            return latestEndpointResponse;
        }
    }
}
