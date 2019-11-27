using CentralDeErros.Api.GraphQL.Types;
using CentralDeErros.Application.Interfaces;
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
                        Name = "onlyNotArchieved"
                    }
                }),
                resolve: context =>
                {
                    var user = (ClaimsPrincipal)context.UserContext;
                    
                    var archieved = context.GetArgument<bool?>("onlyNotArchieved");

                    if (archieved.HasValue) {  //just an example
                        return errorLogAppService.GetAll().Where(e => e.Archieved != archieved.Value);
                    }
                    return errorLogAppService.GetAll();
                }
            );
        }
    }
}
