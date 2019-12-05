using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.Cross.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CentralDeErros.Api.Controllers.Base
{

    public class BaseController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly AppSettings _appSettings;

        public BaseController(IUserAppService userAppService)
        {
            _userAppService = userAppService;

            _appSettings = new AppSettings();
        }

        protected UserViewModel LoggedUser()
        {
            ClaimsPrincipal currentUser = User;
            var userId = currentUser.Claims.Where(c => c.Type == "id").Select(c => c.Value).SingleOrDefault();

            return _userAppService.GetById(new Guid(userId));
        }

        private string GenerateJWT(UserViewModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKeyJWT);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("id", user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(40),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


        }
    }
}