using CentralDeErros.Data.Repositories.Base;
using CentralDeErros.Domain.Models;
using CentralDeErros.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using CentralDeErros.Data.Context;

namespace CentralDeErros.Data.Repositories
{
    public class UserRepository : RepositoryBase<User> , IUserRepository
    {
        public UserRepository(CentralDeErrosContext context) : base(context)
        {
        }
    }
}
