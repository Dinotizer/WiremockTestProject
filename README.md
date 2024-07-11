# WiremockTestProject
This project is a simple example of the use of Wiremock in an NUnit test project, testing against an online and freely available REST-API: https://reqres.in/

There is a small mixture of tests which go to the above live API, along with mocked examples using Wiremock. 
Of the Wiremock examples, there is:
- Example code which mocks the data in the step itself in the Stap Definitions class.
- More cleanly referencing the mock data in a seperate json file to illustrain greater maintainability.

Also in use, albeit minimally:
- Playwright
- ReqnRoll