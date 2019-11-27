using CentralDeErros.Api.GraphQL.Types;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.ViewModel;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.Api.GraphQL
{
    public class CentralDeErrosMutation : ObjectGraphType
    {
        public CentralDeErrosMutation(IErrorLogAppService errorLogAppService)
        {
            FieldAsync<ErrorLogType>(
                "addErrorLog",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ErrorLogInputType>> { Name = "errorLog" }),
                resolve: async context =>
                {
                    var errorLog = context.GetArgument<ErrorLogViewModel>("errorLog");
                    return await context.TryAsyncResolve(async c => await errorLogAppService.Add(errorLog));
                });

            FieldAsync<ErrorLogType>(
                "archieveErrorLog",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ArchieveErrorLogInputType>> { Name = "errorLog" }),
                resolve: async context =>
                {
                    var errorLog = context.GetArgument<ErrorLogViewModel>("errorLog");
                    return await context.TryAsyncResolve(async c => await errorLogAppService.ArchieveErrorLog(errorLog.Id));
                });
        }
    }
}
