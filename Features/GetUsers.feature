Feature: GetUsers

Get users via the https://reqres.in/ Rest API

Scenario: Get list of users
	Given I send a Get request to the List Users API
	Then the API returns requested user details
