using AutoMapper;
using CentralDeErros.Application.Extensions;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.Services.Base;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.CrossCutting.CustomTypes;
using CentralDeErros.Domain.Interfaces.Services;
using CentralDeErros.Domain.Models;
using System;
using System.Collections.Generic;

namespace CentralDeErros.Application.Services
{
    public class ErrorLogAppService : AppServiceBase<ErrorLogViewModel, ErrorLog>, IErrorLogAppService
    {
        public ErrorLogAppService(IErrorLogService service, IMapper mapper) : base(service, mapper)
        {

        }

        public ErrorLogViewModel ArchieveErrorLog(Guid id)
        {
            var errorLog = _service.GetById(id);
            if (errorLog == null)
                return null;

            errorLog.Archieved = true;

            return _mapper.Map<ErrorLogViewModel>(_service.Update(errorLog));
        }

        public IList<ErrorLogViewModel> GetErrorLogs(ErrorLogFilter? filter, OrderErrorLogByField? orderBy)
        {
            Func<ErrorLog, bool> predicate = p => true;
            return Find(predicate.FilterErrorLog(filter)).OrderErrorLogBy(orderBy);
        }
    }
}
