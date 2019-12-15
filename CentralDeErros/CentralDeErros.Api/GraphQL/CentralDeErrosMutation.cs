using CentralDeErros.Api.GraphQL.Types;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.CrossCutting.Utils;
using GraphQL;
using GraphQL.Types;
using System;

namespace CentralDeErros.Api.GraphQL
{
    public class CentralDeErrosMutation : ObjectGraphType
    {
        public CentralDeErrosMutation(IErrorLogAppService errorLogAppService, IUserAppService userAppService)
        {
            Field<ErrorLogType>(
                "addErrorLog",
                description: "Mutation utilizada para adicionar logs de erro.",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ErrorLogInputType>> { Name = "errorLog" }),
                resolve: context =>
                {
                    var errorLog = context.GetArgument<ErrorLogViewModel>("errorLog");
                    return errorLogAppService.Add(errorLog);
                });

            Field<ErrorLogType>(
                "archieveErrorLog",
                description: "Mutation utilizada para arquivar logs de erro.",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idErrorLog" }),
                resolve: context =>
                {
                    var idErrorLog = context.GetArgument<Guid>("idErrorLog");
                    if (errorLogAppService.GetById(idErrorLog) == null)
                    {
                        context.Errors.Add(new ExecutionError("Error log not found."));
                        return null;
                    }
                    return errorLogAppService.ArchieveErrorLog(idErrorLog);
                });

            Field<StringGraphType>(
                "deleteErrorLog",
                description: "Mutation utilizada para deletar logs de erro.",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idErrorLog" }),
                resolve: context =>
                {
                    var idErrorLog = context.GetArgument<Guid>("idErrorLog");
                    if (errorLogAppService.GetById(idErrorLog) == null)
                    {
                        context.Errors.Add(new ExecutionError("Error log not found."));
                        return null;
                    }

                    errorLogAppService.Remove(idErrorLog);
                    return "Error log has been deleted.";
                });

            Field<UserType>(
                "addUser",
                description: "Mutation utilizada para adicionar usuários.",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UserInputType>> { Name = "user" }),
                resolve: context =>
                {
                    var user = context.GetArgument<UserViewModel>("user");
                    user.Password = user.Password.ToHashMD5();
                    user.Active = true;

                    var userAlreadyRegistered = userAppService.Find(p => (p.Email == user.Email) || (p.Login == user.Login)).Count > 0;

                    if (userAlreadyRegistered)
                    {
                        context.Errors.Add(new ExecutionError("User is already registered."));
                        return null;
                    }
                
                    return userAppService.Add(user);
                });
        }
    }
}
