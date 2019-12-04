using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentralDeErros.Application.Interfaces.Base
{
    public interface IAppServiceBase<TViewModel, TModel> 
        where TViewModel: class 
        where TModel : class
    {
        IList<TViewModel> GetAll();
        Task<TViewModel> Add(TViewModel obj);
        IList<TViewModel> Find(Func<TModel, bool> predicate);
    }
}
