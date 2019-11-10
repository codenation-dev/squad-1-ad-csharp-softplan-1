using CentralDeErros.Api.GraphQL.Types;
using CentralDeErros.Application.Interfaces;
using CentralDeErros.Application.ViewModel;
using GraphQL.Types;

namespace CentralDeErros.Api.GraphQL
{
    public class CentralDeErrosQuery : ObjectGraphType
    {
        public CentralDeErrosQuery(IErrorLogAppService errorLogAppService)
        {
            Field<ListGraphType<ErrorLogType>>(
                "errorlogs",
                resolve: context => errorLogAppService.GetAll()
            );
        }
    }
}
