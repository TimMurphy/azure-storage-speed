Feature: AppendBlob speed test

Scenario: Happy path
	Given connectionString is secret
	And containerName is speedtests
	And blobName is speedtests
	And stringLength is 100
	And rows 10
	When I call AppendBlobSpeedTest.RunAsync(connectionString, containerName, blobName, stringLength, rows)
	Then 12 speed test results should be returned
