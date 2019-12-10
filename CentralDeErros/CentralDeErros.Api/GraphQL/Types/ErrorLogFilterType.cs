using CentralDeErros.Application.ViewModel;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.Api.GraphQL.Types
{
    public class ErrorLogFilterType : ObjectGraphType<ErrorLogFilter>
    {
        public ErrorLogFilterType()
        {
            Name = "filter";
            Field(p => p.Code);
            Field(p => p.Message);
            Field(p => p.Archieved);
            Field<EnvironmentType>("Environment", resolve: p => p.Source.Environment);
        }
    }
}
