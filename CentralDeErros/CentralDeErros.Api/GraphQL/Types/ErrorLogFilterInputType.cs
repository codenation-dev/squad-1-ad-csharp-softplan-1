using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            Field<BooleanGraphType>("archieved");
            Field<EnvironmentType>("environment");
        }
    }
}
