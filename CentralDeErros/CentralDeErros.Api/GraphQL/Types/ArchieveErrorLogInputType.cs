using GraphQL.Types;

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
