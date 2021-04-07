Feature: LatestEndpointFeature
	Endpoint used to return the latest currency rates

Scenario: Basic user gets multiple currencies
	Given the user wants to retrieve currency conversion for USD, GBP
	When the user sends the request
	Then the response should be successful
	And the rates should only contain USD, GBP
	And the rates should be different from 1.0
	And the date should contain the current date
	And the timestamp is within the last hour

Scenario: Basic user gets a single currency
	Given the user wants to retrieve currency conversion for USD
	When the user sends the request
	Then the response should be successful
	And the rates should only contain USD

Scenario: Basic user gets all currencies
	Given the user wants to retrieve currency conversion without specifying currency pairs
	When the user sends the request
	Then the response should be successful
	And the rates should contain all supported currencies

Scenario: Basic user gets existing and unexisting currencies
	Given the user wants to retrieve currency conversion for BTC, DOGE
	When the user sends the request
	Then the response should be successful
	And the rates should only contain BTC

Scenario: Basic user gets unexisting currency
	Given the user wants to retrieve currency conversion for DOGE
	When the user sends the request
	Then the response should be an error
	And the message should "You have provided one or more invalid Currency Codes. [Required format: currencies=EUR,USD,GBP,...]"
	And the code should be "invalid_currency_codes"

Scenario: User sends request without an access key
	Given the user does not provide an access key
	When the user sends the request
	Then the response should be an error
	And the message should "You have not supplied an API Access Key. [Required format: access_key=YOUR_ACCESS_KEY]"
	And the code should be "missing_access_key"