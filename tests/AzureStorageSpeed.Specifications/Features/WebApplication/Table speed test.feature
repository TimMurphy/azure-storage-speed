Feature: Table speed test

Scenario: Happy path
	Given I have navigated to /tables
	And fill in the form with
		| Id               | Text       |
		| ConnectionString | secret     |
		| TableName        | speedtests |
		| StringLength     | 100        |
		| Rows             | 10         |
	When I press Run Speed Tests
	Then the speed test results should be displayed
