﻿Feature: CreateUser

Crete a user via the https://reqres.in/ Rest API

Scenario: Create a new user
	Given I input name "Mike"
	And I input job "Dev"
	When I send request to create user
	Then a user is created
