using CentralDeErros.Api.GraphQL.Types;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.CrossCutting.Utils;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.Api.GraphQL
{
    public class CentralDeErrosMutation : ObjectGraphType
    {
        public CentralDeErrosMutation(IErrorLogAppService errorLogAppService, IUserAppService userAppService)
        {
            Field<ErrorLogType>(
                "addErrorLog",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ErrorLogInputType>> { Name = "errorLog" }),
                resolve: context =>
                {
                    var errorLog = context.GetArgument<ErrorLogViewModel>("errorLog");
                    return errorLogAppService.Add(errorLog);
                });

            Field<ErrorLogType>(
                "archieveErrorLog",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ArchieveErrorLogInputType>> { Name = "errorLog" }),
                resolve: context =>
                {
                    var errorLog = context.GetArgument<ErrorLogViewModel>("errorLog");
                    return errorLogAppService.ArchieveErrorLog(errorLog.Id);
                });

            Field<UserType>(
                "addUser",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UserInputType>> { Name = "user" }),
                resolve: context =>
                {
                    var user = context.GetArgument<UserViewModel>("user");
                    var userViewModel = new UserViewModel()
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Login = user.Login,
                        Password = user.Password.ToHashMD5(),
                        Active = true
                    };

                    //TODO: fazer os devidos tratamentos
                    return userAppService.Add(userViewModel);
                });
        }
    }
}
