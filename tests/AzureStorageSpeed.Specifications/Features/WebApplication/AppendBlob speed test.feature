Feature: AppendBlob speed test

Scenario: Happy path
	Given I have navigated to /appendblobs
	And fill in the form with
		| Id               | Text       |
		| ConnectionString | secret     |
		| ContainerName    | speedtests |
		| BlobName         | speedtests |
		| StringLength     | 100        |
		| Rows             | 10         |
	When I press Run Speed Tests
	Then the speed test results should be displayed
