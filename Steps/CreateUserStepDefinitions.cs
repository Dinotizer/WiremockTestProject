using ReqresAPIClient.Models;
using ReqresAPIClient;
using Reqnroll;
using RestSharp;
using System.Threading.Tasks;
using WireMock.Settings;
using Microsoft.Playwright;

namespace WiremockTestProject.Steps
{
    [Binding]
    public class CreateUserStepDefinitions
    {
        private const string BASE_URL = "https://reqres.in/";
        private readonly RequestCreateUser createUserReq;
        private RestResponse response;

        public CreateUserStepDefinitions(RequestCreateUser createUserReq)
        {
            this.createUserReq = createUserReq;
        }

        [Given("I input name (.*)")]
        public async Task GivenIInputName(string name)
        {
            createUserReq.name = name;
        }

        [Given("I input job (.*)")]
        public async Task GivenIInputJob(string job)
        {
            createUserReq.job = job;
        }

        [When("I send request to create user")]
        public async Task WhenISendRequestToCreateUser()
        {
            var api = new Demo();
            response = await api.CreateNewUser(BASE_URL, createUserReq);
        }

        [Then("a user is created")]
        public async Task ThenAUserIsCreated()
        {
            var content = HandleContent.GetContent<ResponseCreateUser>(response);
            
            //Assertions.ReferenceEquals(response, content);
            Assert.AreEqual(createUserReq.name, content.name);
            Assert.AreEqual(createUserReq.job, content.job);
        }
    }
}
