using AutoMapper;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Domain.Interfaces.Services;
using CentralDeErros.Application.Services.Base;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.Domain.Models;
using CentralDeErros.Domain.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;
using CentralDeErros.Domain.Interfaces.Base;
using System.Threading.Tasks;

namespace CentralDeErros.Application.Services
{
    public class UserAppService : AppServiceBase<UserViewModel, User>, IUserAppService
    {
        public UserAppService(IUserService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
