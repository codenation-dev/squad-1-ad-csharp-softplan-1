using CentralDeErros.Application.Interfaces.Base;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeErros.Application.Interfaces
{
    public interface IUserAppService : IAppServiceBase<UserViewModel, User>
    {
        UserViewModel GetById(Guid id);
    }
}
