using GraphQL.Types;

using Example.GraphQL.Model;
using Example.GraphQL.Data;

namespace Example.GraphQL.Types
{
    public class QueryType : ObjectGraphType<Query>
    {
        public QueryType(IModelData data)
        {
            Name = "Query";

            Field<StringGraphType>(
                "whatever",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id" }
                ),
                resolve: ctx => {
                    var id = ctx.GetArgument<string>("id");
                    return data.Whatever(id);
                }
            );
        }
    }
}
