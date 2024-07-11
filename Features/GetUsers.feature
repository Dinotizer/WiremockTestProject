Feature: GetUsers

Get users via the https://reqres.in/ Rest API
Background: 
	Given the mock server is running

# Without Wiremock
Scenario: Get list of users
	Given I send a Get request to the List Users API
	Then the API returns requested user details

# With Wirmock
Scenario: Get mock list of uesrs
	Given I send a Get request to the List Users API using Wiremock
	Then the API returns requested user details using Wiremock
