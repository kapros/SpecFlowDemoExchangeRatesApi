using BoDi;
using ExchangeRatesApi.AcceptanceTests.Utils;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace ExchangeRatesApi.AcceptanceTests.Hooks
{
    [Binding]
    public class GlobalHooks
    {
        private readonly IObjectContainer _objectContainer;

        public GlobalHooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public static void BeforeAllTests(IObjectContainer objectContainer)
        {
            var config = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile("prod.json")
              .Build();
            objectContainer.RegisterFactoryAs((c) => new Uri(config["baseUrl"]));
            objectContainer.RegisterAccessKeys(config);
        }

        [BeforeScenario]
        public void BeforeEach()
        {
            _objectContainer.RegisterInstanceAs<IRestClient>(new RestClient(_objectContainer.Resolve<Uri>()));
        }
    }
}
