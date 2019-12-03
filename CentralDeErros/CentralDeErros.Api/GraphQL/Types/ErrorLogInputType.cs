using GraphQL.Types;

namespace CentralDeErros.Api.GraphQL.Types
{
    public class ErrorLogInputType : InputObjectGraphType
    {
        public ErrorLogInputType()
        {
            Name = "errorLogInput";
            Field<IdGraphType>("id");
            Field<StringGraphType>("code");
            Field<StringGraphType>("message");
            Field<StringGraphType>("level");
            Field<StringGraphType>("archieved");
            Field<EnvironmentType>("environment");
        }
    }
}
