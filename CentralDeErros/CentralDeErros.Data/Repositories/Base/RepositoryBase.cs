using CentralDeErros.Domain.Interfaces.Base;
using CentralDeErros.Domain.Interfaces;
using CentralDeErros.Domain.Models.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentralDeErros.Data.Repositories.Base
{
    public class RepositoryBase<TModel> : IRepositoryBase<TModel> where TModel : ModelBase
    {
        protected IMongoCollection<TModel> _contextMongoDB;
        protected IMongoDatabase _database;
        public RepositoryBase(ICentralDeErrosDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public async Task<TModel> Add(TModel obj)
        {
            await _contextMongoDB.InsertOneAsync(obj);
            return obj;
        }

        public IList<TModel> Find(Func<TModel, bool> predicate)
        {
            return _contextMongoDB.Find(p => true).ToList(); //TODO
        }

        public IList<TModel> GetAll()
        {
            return _contextMongoDB.Find(p => true).ToList();
        }

        public TModel GetById(string id)
        {
            return _contextMongoDB.Find<TModel>(p => p.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _contextMongoDB.DeleteOne<TModel>(p => p.Id == id);
        }

        public void Update(TModel obj)
        {
            _contextMongoDB.ReplaceOne(e => e.Id == obj.Id, obj);
        }
    }
}
