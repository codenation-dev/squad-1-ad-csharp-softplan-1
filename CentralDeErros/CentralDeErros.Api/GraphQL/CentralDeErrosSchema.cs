﻿using GraphQL;
using GraphQL.Types;
using System.Diagnostics.CodeAnalysis;

namespace CentralDeErros.Api.GraphQL
{
    [ExcludeFromCodeCoverage]
    public class CentralDeErrosSchema : Schema
    {
        public CentralDeErrosSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<CentralDeErrosQuery>();
            Mutation = resolver.Resolve<CentralDeErrosMutation>();
        }
    }
}
