using CentralDeErros.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentralDeErros.Application.Interfaces
{
    public interface IErrorLogAppService
    {
        IList<ErrorLogViewModel> GetAll();
        ErrorLogViewModel Insert(ErrorLogViewModel errorLogViewModel);
    }
}
