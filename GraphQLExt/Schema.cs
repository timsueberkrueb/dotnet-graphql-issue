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
        public GraphQLSchema(IServiceProvider provider)
            : base(provider)
        {
            Query = provider.GetService<QueryType>();
            Mutation = provider.GetService<MutationType>();
        }
    }
}
