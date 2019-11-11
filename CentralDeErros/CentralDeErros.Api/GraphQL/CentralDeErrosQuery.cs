using CentralDeErros.Api.GraphQL.Types;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.ViewModel;
using GraphQL.Types;
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
                        Name = "onlyShelved"
                    }
                }),
                resolve: context =>
                {
                    var user = (ClaimsPrincipal)context.UserContext;
                    
                    var shelved = context.GetArgument<bool?>("onlyShelved");

                    if (shelved.HasValue) {  //just an example
                        return errorLogAppService.GetAll().Where(e => e.Shelved == shelved.Value);
                    }
                    return errorLogAppService.GetAll();
                }
            );
        }
    }
}
