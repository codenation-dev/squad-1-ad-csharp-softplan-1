using CentralDeErros.Application.ViewModel;
using GraphQL.Types;

namespace CentralDeErros.Api.GraphQL.Types
{
    public class ErrorLogType : ObjectGraphType<ErrorLogViewModel>
    {
        public ErrorLogType()
        {
            Field(e => e.Id, type:typeof(IdGraphType));
            Field(e => e.Code);
            Field(e => e.Message);
            Field(e => e.Level);
            Field(e => e.Archieved);
            Field(e => e.Environment);
        }
    }
}
