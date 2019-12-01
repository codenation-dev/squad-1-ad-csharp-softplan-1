using CentralDeErros.Api.GraphQL.Types;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.ViewModel;
using GraphQL.Types;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                    new QueryArgument<BooleanGraphType>
                    {
                        Name = "onlyNotArchieved"
                    },
                    new QueryArgument<OrderType>{
                        Name = "order"
                    }
                }),
                resolve: context =>
                {
                    var user = (ClaimsPrincipal)context.UserContext;
                    var onlyNotArchieved = context.GetArgument<bool?>("onlyNotArchieved");
                    var order = context.GetArgument<OrderEnum.Order?>("order");

                    if (onlyNotArchieved.HasValue)
                    {
                        return errorLogAppService.Find(p => !p.Archieved);
                    }
                    
                    return errorLogAppService.GetAll();
                }
            );
        }
    }
}
