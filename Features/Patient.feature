@patient
Feature: Patient
	In order to add/edit/delete the patient record
	As a portal admin
	I want to access the dashboard and add/edit/delete record

Scenario Outline: Add Patient Record
	Given I have browser with openemr url
	When I enter username as 'admin1'
	And I enter password as 'pass'
	And I select the language as 'English (Indian)'
	And I click on login
	And I do mousehover on patient-client menu
	And I click on patients menu
	And I click on Add New Patient
	And I fill the form
		| firstname   | lastname   | dob   | gender   | marital_status   |
		| <firstname> | <lastname> | <dob> | <gender> | <marital_status> |
	And I click on create new patient
	And I click on confirm create new patient
	And I handle the alert
	And I close the happy birthday popup
	Then I should get the added recorde as '<expectedvalue>'

	Examples:
		| firstname | lastname | dob        | gender | marital_status   | expectedvalue                        |
		| bala      | dina     | 2021-04-01 | Male   | Domestic Partner | Medical Record Dashboard - Bala Dina |
		| john      | cena     | 2021-03-01 | Male   | Domestic Partner | Medical Record Dashboard - John Cena |
