using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeErros.Domain.Interfaces.Base
{
    public interface IRepositoryBase<TModel> where TModel : class
    {
        Task<TModel> Add(TModel obj);
        void Update(TModel obj);
        void Remove(string id);
        TModel GetById(string id);
        IList<TModel> GetAll();
        IList<TModel> Find(Func<TModel, bool> predicate);
    }
}
