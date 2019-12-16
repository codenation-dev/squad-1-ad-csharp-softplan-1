using CentralDeErros.Api.Test.Controllers.Base;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.Application.ViewModel.Authentication;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace CentralDeErros.Api.Test.Controllers
{
    public class ErrorLogControllerTest : BaseControllerTest, IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ErrorLogControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async void Should_return_user_login_information()
        {
            LoginViewModel loginViewModel = new LoginViewModel
            {
                Login = "Admin",
                Password = "agesune1"
            };

            var response = await _client.PostAsJsonAsync("/api/login", loginViewModel);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            UserViewModel userAuthorized = await response.Content.ReadAsAsync<UserViewModel>();

            Assert.NotNull(userAuthorized);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", userAuthorized.AccessToken);

            response = await _client.GetAsync("/api/errorlog");

            List<UserViewModel> users = await response.Content.ReadAsAsync<List<UserViewModel>>();

            Assert.NotNull(users);
        }
    }
}
