using Microsoft.Playwright;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private dynamic mockResponse;

        public GetUsersStepDefinitions(Users users)
        {
            this.users = users;
        }

        /// <summary>
        ///  Without Wiremock
        /// </summary>
        [Given("I send a Get request to the List Users API")]
        public async Task GivenISendAGetRequestToTheListUsersAPI()
        {
            var api = new Demo();
            response = await api.GetUsers(BASE_URL, users);
        }

        [Then("the API returns requested user details")]
        public async Task ThenTheAPIReturnsRequestedUserDetails()
        {
            var content = HandleContent.GetContent<Users>(response);
            Assertions.ReferenceEquals(response, content);

            //Assert.AreEqual(2, content.page);
        }

        /// <summary>
        ///  With Wiremock
        /// </summary>
        [Given("the mock server is running")]
        public async Task GivenTheMockServerIsRunning()
        {
            // Refer to the file path relative to the output directory
            string mockResponseJson = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "MockData", "GetUsersMockResponse.json"));
            mockResponse = JsonConvert.DeserializeObject<dynamic>(mockResponseJson);

            // Extract data from the JSON object
            string path = mockResponse.path;
            string method = mockResponse.method;
            int statusCode = mockResponse.status;
            var headers = mockResponse.headers.ToObject<Dictionary<string, string>>();
            string body = JsonConvert.SerializeObject(mockResponse.body);

            // Set up a mock response for the specified request
            var requestBuilder = Request.Create().WithPath(path).UsingMethod(method);
            var responseBuilder = Response.Create().WithStatusCode(statusCode).WithBody(body);

            foreach (var header in headers)
            {
                responseBuilder.WithHeader(header.Key, header.Value);
            }

            server.Given(requestBuilder).RespondWith(responseBuilder);

            /// <summary>
            ///  You can add the content in the step like this, but it isn;t as maintainable. The above does the same but the below content is in a mock data json file instead.
            /// </summary>
            //server.Given(Request.Create().WithPath("/api/users/2").UsingGet())
            //  .RespondWith(Response.Create()
            //  .WithStatusCode(200)
            //  .WithHeader("Content-Type", "application/json")
            //  .WithBody(@"{
            //      ""data"": {
            //          ""id"": 2,
            //          ""email"": ""janet.weaver@reqres.in"",
            //          ""first_name"": ""Janet"",
            //          ""last_name"": ""Weaver"",
            //          ""avatar"": ""https://reqres.in/img/faces/2-image.jpg""
            //      },
            //      ""support"": {
            //          ""url"": ""https://reqres.in/#support-heading"",
            //          ""text"": ""To keep ReqRes free, contributions towards server costs are appreciated!""
            //      }
            //  }"));
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
            //NUnit Assert
            //Assert.AreEqual((int)mockResponse.status, (int)response.StatusCode);
            //Playwright Assert
            Assertions.ReferenceEquals(mockResponse, response);

            // Parse JSON objects to compare them
            var expectedJson = JObject.Parse(JsonConvert.SerializeObject(mockResponse.body));
            var actualJson = JObject.Parse(response.Content);

            //NUnit Assert
            //Assert.AreEqual(expectedJson.ToString(), actualJson.ToString());
            //Playwright Assert
            Assertions.ReferenceEquals(expectedJson, actualJson);

            /// <summary>
            ///  You can add the content in the step like this, but it isn;t as maintainable. The above does the same but the below content is in a mock data json file instead.
            /// </summary>
            //Assert.AreEqual(200, (int)response.StatusCode);
            //var expectedBody = @"{
            //      ""data"": {
            //          ""id"": 2,
            //          ""email"": ""janet.weaver@reqres.in"",
            //          ""first_name"": ""Janet"",
            //          ""last_name"": ""Weaver"",
            //          ""avatar"": ""https://reqres.in/img/faces/2-image.jpg""
            //      },
            //      ""support"": {
            //          ""url"": ""https://reqres.in/#support-heading"",
            //          ""text"": ""To keep ReqRes free, contributions towards server costs are appreciated!""
            //      }
            //  }";
            ////Assert.AreEqual(expectedBody, response.Content);
            //Assertions.ReferenceEquals(expectedBody,response.Content);
        }
    }
}
