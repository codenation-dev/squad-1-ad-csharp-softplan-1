﻿using CentralDeErros.Api.Controllers.Base;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.CrossCutting.CustomTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            var code = HttpContext.Request.Query["code"];
            var message = HttpContext.Request.Query["message"];
            var level = HttpContext.Request.Query["level"];
            var archieved = HttpContext.Request.Query["archieved"];
            var origin = HttpContext.Request.Query["origin"];
            var environment = HttpContext.Request.Query["environment"];
            var orderby = HttpContext.Request.Query["orderby"];

            ErrorLogFilter filter = new ErrorLogFilter();

            if (code.Count > 0)
                filter.Code = code;
            if (message.Count > 0)
                filter.Message = message;
            if (level.Count > 0)
                filter.Level = level;
            if (origin.Count > 0)
                filter.Origin = origin;
            if (archieved.Count > 0)
                filter.Archieved = archieved == "true" ? true : false;

            if (environment.Count > 0)
            {
                if (environment.ToString().ToLower() == "unknown")
                    filter.Environment = ServerEnvironment.Unknown;
                else if (environment.ToString().ToLower() == "development")
                    filter.Environment = ServerEnvironment.Development;
                else if (environment.ToString().ToLower() == "test")
                    filter.Environment = ServerEnvironment.Test;
                else if (environment.ToString().ToLower() == "acceptance")
                    filter.Environment = ServerEnvironment.Acceptance;
                else if (environment.ToString().ToLower() == "production")
                    filter.Environment = ServerEnvironment.Production;
                else
                    return BadRequest("Filtros informados são inválidos!");
            }

            OrderErrorLogByField orderErrorLogBy = OrderErrorLogByField.Any;

            if (orderby.Count > 0)
            {
                if (orderby.ToString().ToLower() == "code")
                    orderErrorLogBy = OrderErrorLogByField.Code;
                else if (orderby.ToString().ToLower() == "level")
                    orderErrorLogBy = OrderErrorLogByField.Level;
                else if (orderby.ToString().ToLower() == "message")
                    orderErrorLogBy = OrderErrorLogByField.Message;
                else
                    return BadRequest("Ordenação inválida!");
            }

            return Ok(_errorLogAppService.GetErrorLogs(filter, orderErrorLogBy));
        }

        /// <summary>
        /// Retorna um ErroLogViewModel a partir do Id informado
        /// </summary>
        /// <returns>Retorna um ErroLogViewModel</returns>
        /// <response code="200">Retorna o view model do logs de erro</response>        
        /// <response code="204">Usuário não encontrado</response>  
        [HttpGet("{id}")]
        public ActionResult<ErrorLogViewModel> GetById(Guid id)
        {
            ErrorLogViewModel errorLog = _errorLogAppService.GetById(id);

            if (errorLog == null)
            {
                return NoContent();
            }
            return Ok(_errorLogAppService.GetById(id));
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
        [Authorize(Roles = UserRoles.ADMIN)]
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

        /// <summary>
        /// Arquiva o log de erro com o Guid passado
        /// </summary>
        /// <returns>O log de erro arquivado</returns>
        /// <response code="200">Retorna uma confirmação de que o log de erro foi arquivado</response>
        /// <response code="404">Retorna uma mensagem de que o log de erro com o Guid passado não foi encontrado</response>
        [HttpPut("archive/{id}")]
        [Authorize(Roles = UserRoles.ADMIN)]
        public ActionResult<ErrorLogViewModel> Archive(Guid id)
        { 
            var log = _errorLogAppService.Find(e => e.Id == id);
            if (log.Count > 0)
            {
                return Ok(_errorLogAppService.ArchieveErrorLog(id));
            }
            return NotFound("Log de erro não encontrado para o Id " + id);
        }
    }
}
