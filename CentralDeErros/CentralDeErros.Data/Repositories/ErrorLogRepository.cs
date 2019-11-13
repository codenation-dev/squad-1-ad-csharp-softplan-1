
using CentralDeErros.Data.Repositories.Base;
using CentralDeErros.Domain.Interfaces;
using CentralDeErros.Domain.Models;

namespace CentralDeErros.Data.Repositories
{
    public class ErrorLogRepository : RepositoryBase<ErrorLog>, IErrorLogRepository
    {
        public ErrorLogRepository(ICentralDeErrosDatabaseSettings settings) : base(settings)
        {
            _contextMongoDB = _database.GetCollection<ErrorLog>(settings.ErrorLogCollectionName);
        }
    }
}
