using CentralDeErros.Application.Interfaces.Base;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.CrossCutting.CustomTypes;
using CentralDeErros.Domain.Models;
using System;
using System.Collections.Generic;

namespace CentralDeErros.Application.Interfaces
{
    public interface IErrorLogAppService : IAppServiceBase<ErrorLogViewModel, ErrorLog>
    {
        ErrorLogViewModel ArchieveErrorLog(Guid id);
        IList<ErrorLogViewModel> GetErrorLogs(ErrorLogFilter? filter, OrderErrorLogByField? orderBy);
    }
}
