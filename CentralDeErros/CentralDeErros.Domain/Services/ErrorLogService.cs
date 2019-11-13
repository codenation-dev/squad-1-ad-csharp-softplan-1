using CentralDeErros.Domain.Interfaces;
using CentralDeErros.Domain.Interfaces.Services;
using CentralDeErros.Domain.Models;
using CentralDeErros.Domain.Services.Base;

namespace CentralDeErros.Domain.Services
{
    public class ErrorLogService : ServiceBase<ErrorLog>, IErrorLogService
    {
        //private readonly IErrorLogRepository _errorLogRepository;
        public ErrorLogService(IErrorLogRepository errorRepository): base(errorRepository)
        {
           // _errorLogRepository = errorRepository;
        }
    }
}
