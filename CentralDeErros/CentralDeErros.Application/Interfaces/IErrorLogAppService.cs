using CentralDeErros.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeErros.Application.Interfaces
{
    public interface IErrorLogAppService
    {
        IList<ErrorLogViewModel> GetAll();
        Task<ErrorLogViewModel> Add(ErrorLogViewModel errorLogViewModel);
    }
}
