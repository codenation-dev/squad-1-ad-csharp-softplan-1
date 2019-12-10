using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentralDeErros.Domain.Interfaces.Base
{
    public interface IServiceBase<TModel> where TModel : class
    {
        TModel Add(TModel obj);
        TModel Update(TModel obj);
        void Remove(Guid id);
        TModel GetById(Guid id);
        IList<TModel> GetAll();
        IList<TModel> Find(Func<TModel, bool> predicate);
    }
}
