using CentralDeErros.Domain.Interfaces;
using CentralDeErros.Domain.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace CentralDeErros.Domain.Services
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly IMongoCollection<ErrorLog> _errorLogs;

        public ErrorLogService(ICentralDeErrosDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _errorLogs = database.GetCollection<ErrorLog>(settings.ErrorLogCollectionName);
        }

        public ErrorLog Insert(ErrorLog errorLog)
        {
            _errorLogs.InsertOne(errorLog);
            return errorLog;
        }

        public IList<ErrorLog> Get() =>
            _errorLogs.Find(errorLog => true).ToList();
    }
}
