using Microsoft.Playwright;
using Reqnroll;
using ReqresAPIClient;
using ReqresAPIClient.Models;
using RestSharp;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace WiremockTestProject.Steps
{
    [Binding]
    public class GetUsersStepDefinitions : WiremockBaseTest
    {
        private const string BASE_URL = "https://reqres.in/";
        private readonly Users users;
        private RestResponse response;
        //private IRestResponse response;

        public GetUsersStepDefinitions(Users users)
        {
            Setup();
            this.users = users;
        }

        /*
        Without Wiremock
        */
        //[Given("I send a Get request to the List Users API")]
        //public async Task GivenISendAGetRequestToTheListUsersAPI()
        //{
        //    var api = new Demo();
        //    response = await api.GetUsers(BASE_URL, users);
        //}

        //[Then("the API returns requested user details")]
        //public async Task ThenTheAPIReturnsRequestedUserDetails()
        //{
        //    var content = HandleContent.GetContent<Users>(response);
        //    Assertions.ReferenceEquals(response, content);

        //    //Assert.AreEqual(2, content.page);
        //}


        /*
        with Wiremock
        */
        [Given("the mock server is running")]
        public async Task GivenTheMockServerIsRunning()
        {
            server.Given(Request.Create().WithPath("/api/users/2").UsingGet())
              .RespondWith(Response.Create()
              .WithStatusCode(200)
              .WithHeader("Content-Type", "application/json")
              .WithBody(@"{
                  ""data"": {
                      ""id"": 2,
                      ""email"": ""janet.weaver@reqres.in"",
                      ""first_name"": ""Janet"",
                      ""last_name"": ""Weaver"",
                      ""avatar"": ""https://reqres.in/img/faces/2-image.jpg""
                  },
                  ""support"": {
                      ""url"": ""https://reqres.in/#support-heading"",
                      ""text"": ""To keep ReqRes free, contributions towards server costs are appreciated!""
                  }
              }"));
        }

        [Given("I send a Get request to the List Users API using Wiremock")]
        public async Task GivenISendAGetRequestToTheListUsersAPIUsingWiremock()
        {
            var client = new RestClient("http://localhost:9091");
            var request = new RestRequest("/api/users/2", Method.Get);
            response = client.Execute(request);
        }

        [Then("the API returns requested user details using Wiremock")]
        public async Task ThenTheAPIReturnsRequestedUserDetailsUsingWiremock()
        {
            Assert.AreEqual(200, (int)response.StatusCode);
            var expectedBody = @"{
                  ""data"": {
                      ""id"": 2,
                      ""email"": ""janet.weaver@reqres.in"",
                      ""first_name"": ""Janet"",
                      ""last_name"": ""Weaver"",
                      ""avatar"": ""https://reqres.in/img/faces/2-image.jpg""
                  },
                  ""support"": {
                      ""url"": ""https://reqres.in/#support-heading"",
                      ""text"": ""To keep ReqRes free, contributions towards server costs are appreciated!""
                  }
              }";
            //Assert.AreEqual(expectedBody, response.Content);
            Assertions.ReferenceEquals(expectedBody,response.Content);
        }

        ~GetUsersStepDefinitions()
        {
            TearDown();
        }
    }
}
