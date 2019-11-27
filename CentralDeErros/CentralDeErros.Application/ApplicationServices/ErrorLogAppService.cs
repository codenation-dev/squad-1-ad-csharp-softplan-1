using AutoMapper;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.Domain.Interfaces.Services;
using CentralDeErros.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentralDeErros.Application.ApplicationServices
{
    public class ErrorLogAppService : IErrorLogAppService
    {
        private readonly IErrorLogService _errorLogService;
        private readonly IMapper _mapper;
        public ErrorLogAppService(IErrorLogService errorLogService, IMapper mapper)
        {
            _errorLogService = errorLogService;
            _mapper = mapper;
        }
        public IList<ErrorLogViewModel> GetAll()
        {
            IList<ErrorLog> errorLogList = _errorLogService.GetAll();
            return _mapper.Map<IList<ErrorLogViewModel>>(errorLogList);
        }

        public async Task<ErrorLogViewModel> Add(ErrorLogViewModel errorLogViewModel)
        {
            ErrorLog errorLog = _mapper.Map<ErrorLog>(errorLogViewModel);
            return _mapper.Map<ErrorLogViewModel>(await _errorLogService.Add(errorLog));
        }

        public async Task<ErrorLogViewModel> ArchieveErrorLog(Guid id)
        {
            var errorLog = await _errorLogService.GetById(id);
            errorLog.Archieved = true;

            return _mapper.Map<ErrorLogViewModel>(await _errorLogService.Update(errorLog));
        }
    }
}
