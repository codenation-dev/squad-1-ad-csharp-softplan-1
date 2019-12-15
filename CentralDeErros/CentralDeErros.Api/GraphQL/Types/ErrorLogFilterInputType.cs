using GraphQL.Types;

namespace CentralDeErros.Api.GraphQL.Types
{
    public class ErrorLogFilterInputType : InputObjectGraphType
    {
        public ErrorLogFilterInputType()
        {
            Name = "filter";
            Field<StringGraphType>("code");
            Field<StringGraphType>("message");
            Field<StringGraphType>("level");
            Field<StringGraphType>("origin");
            Field<BooleanGraphType>("archieved");
            Field<EnvironmentType>("environment");
        }
    }
}
