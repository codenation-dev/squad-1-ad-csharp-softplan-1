using CentralDeErros.Api.Test.Controllers.Base;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.Application.ViewModel.Authentication;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace CentralDeErros.Api.Test.Controllers
{ 
    public class RegistrationControllerTest : BaseControllerTest, IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        ////private readonly HttpClient _client;

        public RegistrationControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
            //_client = factory.CreateClient();
        }

        //[Fact]
        //public async void Should_return_bad_request_when_user_inactive()
        //{
        //    var jsonString = File.ReadAllText("fakeUser.json");

        //    var responserUserCreate = await _client.PostAsJsonAsync("/api/registration", jsonString);

        //    Assert.Equal(HttpStatusCode.OK, responserUserCreate.StatusCode);
        //}
    }
}