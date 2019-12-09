using CentralDeErros.Api.Controllers.Base;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.CrossCutting.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CentralDeErros.CrossCutting.Utils;
using System.Linq;

namespace CentralDeErros.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : BaseController
    {
        private readonly IUserAppService _userAppService;

        public RegistrationController(IUserAppService userAppService) : base(userAppService)
        {
           _userAppService = userAppService;
        }

        [AllowAnonymous]
        [HttpPost()]
        public ActionResult<UserViewModel> Post([FromBody] UserViewModel userViewModel)
        {
            if (userViewModel == null)
            {
                return NoContent();
            }

            var userVerification = _userAppService.Find(p => (p.Email == userViewModel.Email) || (p.Login == userViewModel.Login));


            if (userVerification.Count == 0)
            {
                var userview = new UserViewModel()
                {
                    Name = userViewModel.Name,
                    Email = userViewModel.Email,
                    Login = userViewModel.Login,
                    Password = userViewModel.Password.ToHashMD5(),
                    Active = true
                    
                };

                var user = _userAppService.Add(userview);
                return Ok(user);
               
            }
            else
            {
                return BadRequest("Usuário já cadastrado");
            }
        }
    }
}