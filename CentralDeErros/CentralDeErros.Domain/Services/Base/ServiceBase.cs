using CentralDeErros.Domain.Interfaces.Base;
using CentralDeErros.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentralDeErros.Domain.Services.Base
{
    public class ServiceBase<TModel> : IServiceBase<TModel> where TModel : ModelBase
    {
        protected IRepositoryBase<TModel> _repositoryBase;
        public ServiceBase(IRepositoryBase<TModel> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }
        public async Task<TModel> Add(TModel obj)
        {
            return await _repositoryBase.Add(obj);
        }

        public IList<TModel> Find(Func<TModel, bool> predicate)
        {
            return _repositoryBase.Find(predicate);
        }

        public IList<TModel> GetAll()
        {
            return _repositoryBase.GetAll();
        }

        public async Task<TModel> GetById(Guid id)
        {
            return await _repositoryBase.GetById(id);
        }

        public void Remove(Guid id)
        {
            _repositoryBase.Remove(id);
        }

        public async Task<TModel> Update(TModel obj)
        {
            return await _repositoryBase.Update(obj);
        }
    }
}
