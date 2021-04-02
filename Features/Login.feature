@login
Feature: Login
	In order to manage the complete hospital records
	As a portal user
	I want to access the dashboard

Background: 
	Given I have browser with openemr url

@invalid
Scenario: Invalid Credential
	When I enter username as 'admin123'
	And I enter password as 'pass123'
	And I select the language as 'English (Indian)'
	And I click on login
	Then I should get the error detail as 'Invalid username or password'

@valid
Scenario Outline: Valid Credential
	When I enter username as '<username>'
	And I enter password as '<password>'
	And I select the language as '<language>'
	And I click on login
	Then I should get access to the portal with title as 'OpenEMR'

	Examples:
		| username   | password   | language         |
		| admin      | pass       | English (Indian) |
		| physician  | physician  | English (Indian) |
		| accountant | accountant | English (Indian) |