using CentralDeErros.Domain.Models;
using System.Collections.Generic;

namespace CentralDeErros.Domain.Interfaces
{
    public interface IErrorLogService
    {
        ErrorLog Insert(ErrorLog errorLog);
        IList<ErrorLog> Get();
    }
}
