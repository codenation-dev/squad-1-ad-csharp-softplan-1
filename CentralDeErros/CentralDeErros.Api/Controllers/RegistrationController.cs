using CentralDeErros.Api.Controllers.Base;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.CrossCutting.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// Realiza o cadastro de um novo usuário com base num View Model de usuário
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns>Um view model do usuário cadastrado</returns>
        /// <response code="200">Retorna um view model do usuário que foi cadastrado</response>
        /// <response code="204">Caso o usuário seja nulo, responde com NoContent</response>
        /// <response code="400">Se o usuário passado já estiver cadastrado, resulta num BadRequest</response>
        [HttpPost()]
        public ActionResult<UserViewModel> Post(UserViewModel userViewModel)
        {
            if (userViewModel == null)
                return NoContent();

            if (userViewModel.Email == null || userViewModel.Login == null || userViewModel.Role == null
                || userViewModel.Name == null || userViewModel.Password == null)
                return BadRequest("Campos obrigatórios não informados.");

            var userVerification = _userAppService.Find(p => (p.Email == userViewModel.Email) || (p.Login == userViewModel.Login));

            if (userVerification.Count > 0)
                return BadRequest("Usuário já cadastrado");

            var userview = new UserViewModel()
            {
                Name = userViewModel.Name,
                Email = userViewModel.Email,
                Login = userViewModel.Login,
                Password = userViewModel.Password.ToHashMD5(),
                Active = true
            };

            return Ok(_userAppService.Add(userview));
        }
    }
}