using AutoMapper;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.Domain.Interfaces;
using CentralDeErros.Domain.Models;
using System;
using System.Collections.Generic;

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
            IList<ErrorLog> errorLogList = _errorLogService.Get();
            return _mapper.Map<IList<ErrorLogViewModel>>(errorLogList);
        }

        public ErrorLogViewModel Insert(ErrorLogViewModel errorLogViewModel)
        {
            ErrorLog errorLog = _mapper.Map<ErrorLog>(errorLogViewModel);
            return _mapper.Map<ErrorLogViewModel>(_errorLogService.Insert(errorLog));
        }
    }
}
