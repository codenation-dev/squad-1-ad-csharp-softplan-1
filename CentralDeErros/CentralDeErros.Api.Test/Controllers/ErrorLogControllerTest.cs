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
        public async void Should_Get_ErrosList()
        {
            var user = await GetAuthorizedUser();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", user.AccessToken);

            var response = await _client.GetAsync("/api/errorlog");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Should_Insert_new_ErrorLog()
        {
            var user = await GetAuthorizedUser();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", user.AccessToken);

            ErrorLogViewModel errorLog = new ErrorLogViewModel
            {
                Archieved = false,
                Code = "400",
                Level = "Warning",
                Message = "Error Test"
            };

            var response = await _client.PostAsJsonAsync("/api/errorlog", errorLog);
            ErrorLogViewModel newErrorLog = await response.Content.ReadAsAsync<ErrorLogViewModel>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(newErrorLog);
        }

        [Fact]
        public async void Should_delete_ErrorLog()
        {
            var user = await GetAuthorizedUser();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", user.AccessToken);

            ErrorLogViewModel errorLog = new ErrorLogViewModel
            {
                Archieved = false,
                Code = "400",
                Level = "Warning",
                Message = "Error Test"
            };

            var response = await _client.PostAsJsonAsync("/api/errorlog", errorLog);
            ErrorLogViewModel newErrorLog = await response.Content.ReadAsAsync<ErrorLogViewModel>();
            response = await _client.DeleteAsync($"/api/errorlog/{newErrorLog.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response = await _client.GetAsync($"/api/errorlog/{newErrorLog.Id}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
