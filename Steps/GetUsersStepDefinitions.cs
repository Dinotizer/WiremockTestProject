using Microsoft.Playwright;
using Reqnroll;
using ReqresAPIClient;
using ReqresAPIClient.Models;
using RestSharp;

namespace WiremockTestProject.Steps
{
    [Binding]
    public class GetUsersStepDefinitions
    {
        private const string BASE_URL = "https://reqres.in/";
        private readonly Users users;
        private RestResponse response;

        public GetUsersStepDefinitions(Users users)
        {
            this.users = users;
        }

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
    }
}
