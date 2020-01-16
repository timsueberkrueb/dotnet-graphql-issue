using GraphQL.Types;

using Example.GraphQL.Model;
using Example.GraphQL.Data;

namespace Example.GraphQL.Types
{
    public class MutationType : ObjectGraphType<Mutation>
    {
        public MutationType(IModelData data)
        {
            Name = "Mutation";

            Field<StringGraphType>(
                "addFoo",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<FooInputType>> { Name = "foo" },
                    new QueryArgument<NonNullGraphType<BarInputType>> { Name = "bar" },
                    new QueryArgument<NonNullGraphType<BazInputType>> { Name = "baz" }
                ),
                resolve: ctx => {
                    var foo = ctx.GetArgument<FooInput>("foo");
                    var bar = ctx.GetArgument<BarInput>("bar");
                    var baz = ctx.GetArgument<BazInput>("baz");
                    return data.AddFoo(foo, bar, baz);
                }
            );
        }
    }
}
