Feature: Table speed test

Scenario: Happy path
	Given connectionString is secret
	And tableName is speedtest
	And stringLength is 100
	And rows 10
	When I call TableSpeedTest.RunAsync(connectionString, tableName, stringLength, rows)
	Then 12 speed test results should be returned
