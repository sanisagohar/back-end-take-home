### How to run this application

- Visual Studio 2017 will be needed to run this application.
- Open solution file 'SearchEngine.Api.sln' in SearchEngine.Api folder in Visual Studio.
- Open Web.Config file of project 'SearchEngine.Api' and set folder path of data i.e. csv files in for Key 'DataRepositoryPath'.
- Right click this project and select 'Set As StartUp Project' and Run (press F5)
- Once application is launched in browser and you can see ASP.net page, end point can be test through any API testing tool. I used Postman and also checked-in Postman collection on Git.
- API end point is /api/search?origin=Abidjan&destination=kn

### How to run unit tests

- Open App.Config file of project 'SearchEngine.Api.Tests' and set folder path of data i.e. csv files for Key 'DataRepositoryPath'.
- Build application in Visual Studio by 'Build -> Build Solution' or by pressing F6.
- Open Test Explorer by 'Test -> Windows -> Test Explorer' or by pressing Ctrl+E,T.
- Test Explorer will be listing all unit tests in ServiceTest.cs in test project 'SearchEngine.Api.Tests'
- Click Run All to run all the tests in there.

### Assumptions

- User can enter any of the following value in source or destination parameter:
	- Aiport name
	- Country
	- City
	- IATA 3

  Application will first exact match any of the above string to get source and destination, if no source or destination found it would search as a substring and get first match.
- Maximum number of routes is 5 to prevent application to run indefinitely.


### Correction in data

- Removed trailing space in Latitude column to be read in CSV otherwise application will throw exception.
- Corrected Air Chine 2 digit code as CA as CA is being used in routes.csv as Airline Id.