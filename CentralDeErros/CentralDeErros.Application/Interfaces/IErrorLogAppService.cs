using CentralDeErros.Application.Interfaces.Base;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.Domain.Models;
using System;
using System.Threading.Tasks;

namespace CentralDeErros.Application.Interfaces
{
    public interface IErrorLogAppService : IAppServiceBase<ErrorLogViewModel, ErrorLog>
    {
        ErrorLogViewModel ArchieveErrorLog(Guid id);
        void DeleteErrorLog(Guid id);
    }
}
