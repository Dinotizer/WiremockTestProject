using System;
using Reqnroll;

namespace WiremockTestProject.Steps
{
    [Binding]
    public class CreateUserStepDefinitions
    {
        [Given("I input name (.*)")]
        public async Task GivenIInputName(string name)
        {
            throw new PendingStepException();
        }

        [Given("I input job (.*)")]
        public async Task GivenIInputJob(string job)
        {
            throw new PendingStepException();
        }

        [When("I send request to create user")]
        public async Task WhenISendRequestToCreateUser()
        {
            throw new PendingStepException();
        }

        [Then("validate user is created")]
        public async Task ThenValidateUserIsCreated()
        {
            throw new PendingStepException();
        }
    }
}
