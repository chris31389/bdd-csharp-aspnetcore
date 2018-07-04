Feature: User
	So that people can use the system
	As a System Admin
	I want to manage other users
		
@HappyPath
Scenario: Create User
	Given a user with the name 'Chris'
	When I create the user
	Then I get a response with a status code of '201' 
	And I get a response with a user
	And that user has a name 'Chris'
