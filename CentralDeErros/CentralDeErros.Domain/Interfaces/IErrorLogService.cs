using CentralDeErros.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentralDeErros.Domain.Interfaces
{
    public interface IErrorLogService
    {
        Task<ErrorLog> Insert(ErrorLog errorLog);
        IList<ErrorLog> Get();
    }
}
