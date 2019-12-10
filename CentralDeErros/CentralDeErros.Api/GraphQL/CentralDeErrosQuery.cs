﻿using CentralDeErros.Api.GraphQL.Filters;
using CentralDeErros.Api.GraphQL.Types;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.ViewModel;
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
                    var orderBy = context.GetArgument<OrderByEnum.OrderBy?>("orderBy");
                    var filter = context.GetArgument<ErrorLogFilter?>("filter");

                    IList<ErrorLogViewModel> errorLogs = errorLogAppService.GetAll();

                    if (orderBy.HasValue)
                        errorLogs = errorLogs.OrderErrorLogBy((OrderByEnum.OrderBy)orderBy);

                    return errorLogs;
                }
            );
        }
    }
}
