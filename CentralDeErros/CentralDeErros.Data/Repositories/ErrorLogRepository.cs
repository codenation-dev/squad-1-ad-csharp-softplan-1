
using CentralDeErros.Data.Context;
using CentralDeErros.Data.Repositories.Base;
using CentralDeErros.Domain.Interfaces;
using CentralDeErros.Domain.Models;

namespace CentralDeErros.Data.Repositories
{
    public class ErrorLogRepository : RepositoryBase<ErrorLog>, IErrorLogRepository
    {
        public ErrorLogRepository(CentralDeErrosContext context) : base(context)
        {

        }
    }
}
