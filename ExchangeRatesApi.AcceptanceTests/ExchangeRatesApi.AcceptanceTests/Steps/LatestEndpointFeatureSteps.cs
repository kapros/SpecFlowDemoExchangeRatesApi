using BoDi;
using ExchangeRatesApi.AcceptanceTests.Endpoints;
using ExchangeRatesApi.AcceptanceTests.Endpoints.Latest;
using ExchangeRatesApi.AcceptanceTests.Utils;
using FluentAssertions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace ExchangeRatesApi.AcceptanceTests.Steps
{
    [Binding]
    public class LatestEndpointFeatureSteps
    {
        private const string SCENARIO_USER = "basicUser";

        private readonly IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;
        private BaseResponse<LatestEndpointResponse> _response => _objectContainer.Resolve<BaseResponse<LatestEndpointResponse>>();

        public LatestEndpointFeatureSteps(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
            _scenarioContext.Add("user", _objectContainer.GetAccessKeys()[SCENARIO_USER]);
        }

        [Given(@"the user wants to retrieve currency conversion for (.*)")]
        public void AddCurrencyPairs(string currencySymbols)
        {
            _scenarioContext.Add("currencies", currencySymbols.Split(",").Select(x => x.Trim()));
        }

        [Given(@"the user wants to retrieve currency conversion without specifying currency pairs")]
        public void AddNoCurrencyPairs()
        {
            _scenarioContext.Add("currencies", Array.Empty<string>());
        }

        [Given(@"the user does not provide an access key")]
        public void GivenTheUserDoesNotProvideAnAccessKey()
        {
            _scenarioContext.Add("currencies", Array.Empty<string>());
            _scenarioContext.Remove("user");
        }

        [When(@"the user sends the request")]
        public async Task SendRequest()
        {
            var endpoint = new Latest(_objectContainer.Resolve<IRestClient>());
            var response = await endpoint.SendGet((string)_scenarioContext.GetValueOrDefault("user"), _scenarioContext.Get<IEnumerable<string>>("currencies"));
            _objectContainer.RegisterInstanceAs(response);
        }

        [Then(@"the response should be successful")]
        public void AssertSuccess()
        {
            _response.SuccessfulResponse.Success.Should().BeTrue();
        }

        [Then(@"the rates should only contain (.*)")]
        public void AssertRatesOnlyContainSpecificCurrencies(string currencySymbols)
        {
            var expectedCurrencies = currencySymbols.Split(",").Select(x => x.Trim());
            _response.SuccessfulResponse.Rates.Keys.Should().Contain(expectedCurrencies);
        }

        [Then(@"the rates should be different from (.*)")]
        public void ThenTheRatesShouldBeDifferentFrom(float referenceValue)
        {
            _response.SuccessfulResponse.Rates.Should().NotContainValues(referenceValue);
        }

        [Then(@"the date should contain the current date")]
        public void AssertResponseHasTodaysDate()
        {
            _response.SuccessfulResponse.Date.Should().Be(DateTime.UtcNow.Date);
        }

        [Then(@"the timestamp is within the last hour")]
        public void AssertTimestampIsWithinHour()
        {
            _response.SuccessfulResponse.TimeStamp.Should().BeGreaterOrEqualTo(new DateTimeOffset(DateTime.UtcNow.AddHours(-1)).ToUnixTimeSeconds());
        }

        [Then(@"the rates should contain all supported currencies")]
        public void AssertRatesContainsAllCurrencies()
        {
            _response.SuccessfulResponse.Rates.Keys.Should().HaveCount(168);
        }

        [Then(@"the response should be an error")]
        public void AssertResponseIsAnError()
        {
            _response.Error.Should().NotBeNull();
        }

        [Then(@"the message should ""(.*)""")]
        public void AssertErrorMessage(string message)
        {
            _response.Error.Message.Should().Be(message);
        }

        [Then(@"the code should be ""(.*)""")]
        public void AssertErrorCode(string code)
        {
            _response.Error.Code.Should().Be(code);
        }
    }
}
