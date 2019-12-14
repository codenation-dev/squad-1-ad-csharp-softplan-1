using CentralDeErros.Api.Controllers.Base;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CentralDeErros.Api.Controllers
{
    /// <summary>
    /// Este endpoint expõe o CRUD para os logs de erro.
    /// </summary>
    [Route("api/errorlog")]
    [ApiController]
    [Authorize]
    public class ErrorLogController : BaseController
    {
        private readonly IErrorLogAppService _errorLogAppService;
        public ErrorLogController(IErrorLogAppService errorLogAppService, IUserAppService userAppService) : base(userAppService)
        {
            _errorLogAppService = errorLogAppService;
        }

        /// <summary>
        /// Retorna uma lista com todos os logs de erro
        /// </summary>
        /// <returns>Uma lista com todos os logs de erro</returns>
        /// <response code="200">Retorna os view models dos logs de erro</response>        
        [HttpGet]
        public ActionResult<ErrorLogViewModel> Get()
        {
            return Ok(_errorLogAppService.GetAll());
        }

        /// <summary>
        /// Inclusão de um novo log de erro
        /// </summary>
        /// <returns>O log de erro salvo</returns>
        /// <response code="200">Retorna o view model do log de erro salvo</response>
        [HttpPost]
        public ActionResult<ErrorLogViewModel> Post([FromBody] ErrorLogViewModel errorLogViewModel)
        {
            return Ok(_errorLogAppService.Add(errorLogViewModel));
        }

        /// <summary>
        /// Deleta o log de erro com o Guid passado
        /// </summary>
        /// <returns>O log de erro deletado</returns>
        /// <response code="200">Retorna uma confirmação de que o log de erro foi deletado</response>
        /// <response code="404">Retorna uma mensagem de que o log de erro com o Guid passado não foi encontrado</response>
        [HttpDelete("{id}")]
        public ActionResult<ErrorLogViewModel> Delete(Guid id)
        {
            var log = _errorLogAppService.Find(e => e.Id == id);
            if (log.Count > 0)
            {
                _errorLogAppService.Remove(id);
                return Ok("Log de erro deletado com sucesso");
            }
            return NotFound("Log de erro não encontrado para o Id " + id);
        }
    }
}
