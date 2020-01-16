using System;

using Microsoft.Extensions.DependencyInjection;

using GraphQL;
using GraphQL.Utilities;
using GraphQL.Types;

using Example.GraphQL.Types;

namespace Example.GraphQL.Ext
{
    public class GraphQLSchema : Schema
    {
        public GraphQLSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<QueryType>();
            Mutation = resolver.Resolve<MutationType>();
        }
    }
}
