using GraphQL.Types;

namespace CentralDeErros.Api.GraphQL.Types
{
    public class ErrorLogInputType : InputObjectGraphType
    {
        public ErrorLogInputType()
        {
            Name = "errorLogInput";
            Field<StringGraphType>("code");
            Field<StringGraphType>("message");
            Field<StringGraphType>("level");
            Field<StringGraphType>("origin");
            Field<StringGraphType>("archieved");
            Field<EnvironmentType>("environment");
        }
    }
}
