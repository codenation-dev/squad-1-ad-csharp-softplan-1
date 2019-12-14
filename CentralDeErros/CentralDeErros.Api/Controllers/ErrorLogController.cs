using CentralDeErros.Api.Controllers.Base;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.CrossCutting.CustomTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using static System.Net.WebRequestMethods;

namespace CentralDeErros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorLogController : BaseController
    {
        private readonly IErrorLogAppService _errorLogAppService;
        public ErrorLogController(IErrorLogAppService errorLogAppService, IUserAppService userAppService) : base(userAppService)
        {
            _errorLogAppService = errorLogAppService;
        }
        // GET: api/ErrorLog
        [HttpGet]
        public ActionResult<ErrorLogViewModel> Get()
        {
            var code = HttpContext.Request.Query["code"];
            var message = HttpContext.Request.Query["message"];
            var level = HttpContext.Request.Query["level"];
            var archieved = HttpContext.Request.Query["archieved"];
            var environment = HttpContext.Request.Query["environment"];
            var orderby = HttpContext.Request.Query["orderby"];

            ErrorLogFilter filter = new ErrorLogFilter();

            if (code.Count > 0)
                filter.Code = code;
            if (message.Count > 0)
                filter.Message = message;
            if (level.Count > 0)
                filter.Level = level;
            if (archieved.Count > 0)
                filter.Archieved = archieved == "true" ? true : false;

            if (environment.Count > 0)
            {
                if (environment.ToString().ToLower() == "Unknown")
                    filter.Environment = ServerEnvironment.Unknown;
                else if (environment.ToString().ToLower() == "Development")
                    filter.Environment = ServerEnvironment.Development;
                else if (environment.ToString().ToLower() == "Test")
                    filter.Environment = ServerEnvironment.Test;
                else if (environment.ToString().ToLower() == "Acceptance")
                    filter.Environment = ServerEnvironment.Acceptance;
                else if (environment.ToString().ToLower() == "Production")
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

        // POST: api/ErrorLog
        [HttpPost]
        public ActionResult<ErrorLogViewModel> Post([FromBody] ErrorLogViewModel errorLogViewModel)
        {
            return Ok(_errorLogAppService.Add(errorLogViewModel));
        }

        // DELETE: api/ApiWithActions/5
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
