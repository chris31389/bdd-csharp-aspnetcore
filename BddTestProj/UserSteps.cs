using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApiSpecflowTest;
using Newtonsoft.Json;
using Xunit;
using Xunit.Gherkin.Quick;

namespace BddTestProj
{
    [FeatureFile("./User.feature")]
    public sealed class UserSteps : Feature
    {
        [Given(@"a user with the name '(\w+)'")]
        public void GivenAUserWithTheNameChris(string name)
        {
            ScenarioContext.Current.Add("create-user", new UserCreateModel {Name = name});
        }

        [When(@"I create the user")]
        public async Task WhenICreateTheUser()
        {
            CustomWebApplicationFactory<Startup> customWebApplicationFactory =
                new CustomWebApplicationFactory<Startup>();
            HttpClient httpClient = customWebApplicationFactory.CreateClient();
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("api/user",
                new StringContent("{\"name\":\"Chris\"}", Encoding.UTF8, "application/json"));

            ScenarioContext.Current.Add("httpResponseMessage", httpResponseMessage);
        }

        [Then(@"I get a response with a status code of '(\d+)'")]
        public void IGetAResponseWithAStatusCode(int statusCode)
        {
            ScenarioContext.Current.TryGetValue("httpResponseMessage", out object httpResponseMessageObject);
            HttpResponseMessage httpResponseMessage = httpResponseMessageObject as HttpResponseMessage;
            Assert.NotNull(httpResponseMessage);

            Assert.Equal((int) httpResponseMessage.StatusCode, statusCode);
        }

        [And(@"I get a response with a user")]
        public async Task IGetAResponseWithAUser()
        {
            ScenarioContext.Current.TryGetValue("httpResponseMessage", out object httpResponseMessageObject);
            HttpResponseMessage httpResponseMessage = httpResponseMessageObject as HttpResponseMessage;
            Assert.NotNull(httpResponseMessage);

            string jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            UserModel userModel = JsonConvert.DeserializeObject<UserModel>(jsonResponse);
            ScenarioContext.Current.Add("user", userModel);
        }

        [And(@"that user has a name '(\w+)'")]
        public void ThenHasAName(string name)
        {
            ScenarioContext.Current.TryGetValue("user", out object userObject);
            UserModel userModel = userObject as UserModel;
            Assert.NotNull(userModel);

            Assert.Equal(userModel.Name, name);
        }
    }
}