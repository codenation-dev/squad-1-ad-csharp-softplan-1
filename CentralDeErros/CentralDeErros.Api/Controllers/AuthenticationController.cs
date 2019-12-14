using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.Application.ViewModel.Authentication;
using CentralDeErros.CrossCutting.Helpers;
using CentralDeErros.CrossCutting.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CentralDeErros.Api.Controllers
{
    /// <summary>
    /// Controlador responsável pelo serviço de autenticação
    /// </summary>
    [Route("api/login")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IUserAppService _userAppService;

        public AuthenticationController(IUserAppService userAppService, IOptions<AppSettings> appSettings)
        {
            _userAppService = userAppService;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Endpoint de login do usuário
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns>Um ActionResult</returns>
        /// <response code="200">Usuário logado com sucesso</response>
        /// <response code="400">Se as credenciais passadas não existirem, responde com um BadRequest e a mensagem "Usuário não cadastrado"</response>
        /// <response code="400">Se a senha for inválida, responde com um BadRequest e a mensagem  "Senha inválida"</response>
        /// <response code="400">Se o usuário estiver inativo, responde com um BadRequest e a mensagem "Usuário Inativo"</response>
        [AllowAnonymous]
        [HttpPost()]
        public ActionResult Post(LoginViewModel loginViewModel)
        {
            var user = _userAppService.Find(p => p.Login == loginViewModel.Login).FirstOrDefault();

            if (user == null)
                return BadRequest("Usuário não cadastrado.");

            if (user.Password != loginViewModel.Password.ToHashMD5())
                return BadRequest("Senha inválida.");

            if (!user.Active)
                return BadRequest("Usuário inativo.");

            user.AccessToken = GenerateJWT(user);

            return Ok(user);
        }

        private string GenerateJWT(UserViewModel usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKeyJWT);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Name),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim("id", usuario.Id.ToString()),
                }),

                Expires = DateTime.UtcNow.AddMinutes(30),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}