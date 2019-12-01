using AutoMapper;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.Services.Base;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.Domain.Interfaces.Services;
using CentralDeErros.Domain.Models;
using System;
using System.Threading.Tasks;

namespace CentralDeErros.Application.Services
{
    public class ErrorLogAppService : AppServiceBase<ErrorLogViewModel, ErrorLog>, IErrorLogAppService
    {
        public ErrorLogAppService(IErrorLogService service, IMapper mapper) : base(service, mapper)
        {

        }

        public async Task<ErrorLogViewModel> ArchieveErrorLog(Guid id)
        {
            var errorLog = await _service.GetById(id);
            errorLog.Archieved = true;

            return _mapper.Map<ErrorLogViewModel>(await _service.Update(errorLog));
        }
    }
}
