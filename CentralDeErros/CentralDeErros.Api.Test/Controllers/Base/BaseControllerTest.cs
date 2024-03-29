﻿using CentralDeErros.Api.Controllers.Base;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.Services;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.Application.ViewModel.Authentication;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CentralDeErros.Api.Test.Controllers.Base
{
    public class BaseControllerTest
    {
        private readonly HttpClient _client;

        public BaseControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        protected async Task<UserViewModel> CreateNewUser(string login = "default", string pass = "default",
            string mail = "@mail.com")
        {
            UserViewModel user = new UserViewModel
            {
                Email = $"{login}{mail}",
                Login = login,
                Name = $"Test Name {login}",
                Password = pass,
                Role = "Admin"
            };

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string json = JsonConvert.SerializeObject(user);

            var response = await _client.PostAsJsonAsync("/api/Registration", new StringContent(json, Encoding.UTF8, "application/json"));
            return await response.Content.ReadAsAsync<UserViewModel>();
        }

        protected async Task<UserViewModel> GetAuthorizedUser()
        {
            LoginViewModel loginViewModel = new LoginViewModel
            {
                Login = "Admin",
                Password = "agesune1"
            };

            var response = await _client.PostAsJsonAsync("/api/login", loginViewModel);

            return await response.Content.ReadAsAsync<UserViewModel>();
        }
    }
}