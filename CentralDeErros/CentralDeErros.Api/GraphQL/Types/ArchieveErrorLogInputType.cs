using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralDeErros.Api.GraphQL.Types
{
    public class ArchieveErrorLogInputType : InputObjectGraphType
    {
        public ArchieveErrorLogInputType()
        {
            Name = "archieveErrorLogInput";
            Field<IdGraphType>("id");
        }
    }
}
