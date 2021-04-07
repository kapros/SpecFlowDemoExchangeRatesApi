using BoDi;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace ExchangeRatesApi.AcceptanceTests.Utils
{
    public static class Extensions
    {
        public static void AddApiKey(this IRestRequest req, string apiKey)
        {
            if (!string.IsNullOrWhiteSpace(apiKey))
                req.AddParameter("access_key", apiKey);
        }

        public static void RegisterAccessKeys(this IObjectContainer objectContainer, IConfigurationRoot config)
        {
            var keys = config.GetSection("accessKeys").GetChildren().AsEnumerable().ToDictionary(x => x.Key, x => x.Value);
            objectContainer.RegisterInstanceAs(keys, "accessKeys");
        }

        public static Dictionary<string, string> GetAccessKeys(this IObjectContainer objectContainer) =>
            objectContainer.Resolve<Dictionary<string, string>>("accessKeys");
    }
}
