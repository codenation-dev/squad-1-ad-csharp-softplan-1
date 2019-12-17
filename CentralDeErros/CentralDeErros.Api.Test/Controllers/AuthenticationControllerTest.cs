using CentralDeErros.Api.Test.Controllers.Base;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.Application.ViewModel.Authentication;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Xunit;

namespace CentralDeErros.Api.Test.Controllers
{
    public class AuthenticationControllerTest : BaseControllerTest, IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public AuthenticationControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async void Should_return_user_login_information()
        {
            //UserViewModel user = await CreateNewUser("Jhon", "family");

            LoginViewModel loginViewModel = new LoginViewModel
            {
                Login = "Admin",//user.Login,
                Password = "agesune1"//user.Password
            };

            var response = await _client.PostAsJsonAsync("/api/login", loginViewModel);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            UserViewModel userAuthorized = await response.Content.ReadAsAsync<UserViewModel>();

            Assert.NotNull(userAuthorized);
            Assert.NotNull(userAuthorized.AccessToken);
            Assert.Equal("Administrator", userAuthorized.Name);
            //Assert.Equal(user.Name, userAuthorized.Name);
        }

        [Fact]
        public async void Should_return_bad_request_when_user_not_exists()
        {
            LoginViewModel loginViewModel = new LoginViewModel
            {
                Login = "notExists",
                Password = "agesune1"
            };

            var response = await _client.PostAsJsonAsync("/api/login", loginViewModel);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void Should_return_bad_request_when_invalid_pass()
        {
            LoginViewModel loginViewModel = new LoginViewModel
            {
                Login = "Admin",
                Password = "invalid_pass"
            };

            var response = await _client.PostAsJsonAsync("/api/login", loginViewModel);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        //[Fact]
        //public async void Should_return_bad_request_when_user_inactive()
        //{
        //    UserViewModel user = new UserViewModel
        //    {
        //        Active = false,
        //        Email = "mail1@mailmail.com",
        //        Login = "Inactive_User",
        //        Name = "Inactive User",
        //        Password = "2134"
        //    };

        //    var response = await _client.PostAsJsonAsync("/api/registration", user);
        //    UserViewModel newErrorLog = await response.Content.ReadAsAsync<UserViewModel>();

        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        //    //LoginViewModel loginViewModel = new LoginViewModel
        //    //{
        //    //    Login = "Inactive_User",
        //    //    Password = "2134"
        //    //};

        //    //var response = await _client.PostAsync("/api/login", loginViewModel, new JsonMediaTypeFormatter());
        //    //UserViewModel userAuthorized = await response.Content.ReadAsAsync<UserViewModel>();

        //    //Assert.False(userAuthorized.Active);
        //    //Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        //}
    }
}