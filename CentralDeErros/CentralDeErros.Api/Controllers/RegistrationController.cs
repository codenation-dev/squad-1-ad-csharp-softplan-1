using CentralDeErros.Api.Controllers.Base;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.Cross.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CentralDeErros.Cross.Utils;
using System.Linq;

namespace CentralDeErros.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : BaseController
    {
        //private readonly AppSettings _appSettings;
        private readonly IUserAppService _userAppService;

        public RegistrationController(IUserAppService userAppService) : base(userAppService)
        {
            //_appSettings = appSettings;
           _userAppService = userAppService;
        }

        [AllowAnonymous]
        [HttpPost()]
        public ActionResult<UserViewModel> Post([FromBody] UserViewModel userViewModel)
        {
            //var userViewModel = JsonConvert.DeserializeObject<UserViewModel>(jsonstring);
            if (userViewModel == null)
            {
                return NoContent();
            }

            //var userVerification = _userAppService.Find(p => (p.Email == userViewModel.Email) || (p.Login == userViewModel.Login));
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
                return Ok(user.Result);
               
            }
            else
            {
                return BadRequest("Usuário já cadastrado");
            }
        }
    }
}