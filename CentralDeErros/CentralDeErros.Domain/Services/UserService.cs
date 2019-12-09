using CentralDeErros.Domain.Interfaces.Repositories;
using CentralDeErros.Domain.Interfaces.Services;
using CentralDeErros.Domain.Models;
using CentralDeErros.Domain.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentralDeErros.Domain.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        public UserService(IUserRepository userRepository) : base(userRepository)
        {

        }
    }
}
