using CentralDeErros.Api.GraphQL.Types;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.ViewModel;
using CentralDeErros.CrossCutting.CustomTypes;
using GraphQL.Types;
using System.Collections.Generic;
using System.Security.Claims;

namespace CentralDeErros.Api.GraphQL
{
    public class CentralDeErrosQuery : ObjectGraphType
    {
        public CentralDeErrosQuery(IErrorLogAppService errorLogAppService)
        {
            Field<ListGraphType<ErrorLogType>>(
                "errorlogs",
                description: "Query utilizada para consultar os logs de erro.",
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<ErrorLogFilterInputType>
                    {
                        Name = "filter"
                    },
                    new QueryArgument<OrderByType>{
                        Name = "orderBy"
                    }
                }),
                resolve: context =>
                {
                    var user = (ClaimsPrincipal)context.UserContext;
                    var orderBy = context.GetArgument<OrderErrorLogByField?>("orderBy");
                    var filter = context.GetArgument<ErrorLogFilter?>("filter");

                    return errorLogAppService.GetErrorLogs(filter, orderBy);
                }
            );
        }
    }
}
