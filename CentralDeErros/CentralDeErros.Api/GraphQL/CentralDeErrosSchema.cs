using GraphQL;
using GraphQL.Types;

namespace CentralDeErros.Api.GraphQL
{
    public class CentralDeErrosSchema : Schema
    {
        public CentralDeErrosSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<CentralDeErrosQuery>();
            Mutation = resolver.Resolve<CentralDeErrosMutation>();
        }
    }
}
