using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeErros.Domain.Interfaces.Base
{
    public interface IRepositoryBase<TModel> where TModel : class
    {
        Task<TModel> Add(TModel obj);
        Task<TModel> Update(TModel obj);
        Task Remove(Guid id);
        Task<TModel> GetById(Guid id);
        IList<TModel> GetAll();
        IList<TModel> Find(Func<TModel, bool> predicate);
    }
}
