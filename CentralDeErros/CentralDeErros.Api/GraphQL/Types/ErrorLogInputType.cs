using GraphQL.Types;
using System.Diagnostics.CodeAnalysis;

namespace CentralDeErros.Api.GraphQL.Types
{
    [ExcludeFromCodeCoverage]
    public class ErrorLogInputType : InputObjectGraphType
    {
        public ErrorLogInputType()
        {
            Name = "errorLogInput";
            Field<StringGraphType>("code");
            Field<StringGraphType>("message");
            Field<StringGraphType>("level");
            Field<StringGraphType>("archieved");
            Field<EnvironmentType>("environment");
        }
    }
}
