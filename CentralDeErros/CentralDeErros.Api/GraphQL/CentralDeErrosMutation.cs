using CentralDeErros.Api.GraphQL.Types;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.CrossCutting.Utils;
using GraphQL;
using GraphQL.Types;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CentralDeErros.Api.GraphQL
{
    [ExcludeFromCodeCoverage]
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
                        context.Errors.Add(new ExecutionError("Log de erro não encontrado."));
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
                        context.Errors.Add(new ExecutionError("Log de erro não encontrado."));
                        return null;
                    }

                    errorLogAppService.Remove(idErrorLog);
                    return "Log de erro deletado com sucesso.";
                });

            Field<UserType>(
                "addUser",
                description: "Mutation utilizada para adicionar usuários.",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UserInputType>> { Name = "user" }),
                resolve: context =>
                {
                    var user = context.GetArgument<UserViewModel>("user");
                    
                    if (user.Email == null || user.Login == null || user.Role == null
                        || user.Name == null || user.Password == null)
                    {
                        context.Errors.Add(new ExecutionError("Campos obrigatórios não informados."));
                        return null;
                    }
                    
                    user.Password = user.Password.ToHashMD5();
                    user.Active = true;

                    var userAlreadyRegistered = userAppService.Find(p => (p.Email == user.Email) || (p.Login == user.Login)).Count > 0;

                    if (userAlreadyRegistered)
                    {
                        context.Errors.Add(new ExecutionError("Usuário já cadastrado."));
                        return null;
                    }
                
                    return userAppService.Add(user);
                });
        }
    }
}
