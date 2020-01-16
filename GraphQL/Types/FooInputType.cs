using GraphQL.Types;

using Example.GraphQL.Model;
using Example.GraphQL.Data;

namespace Example.GraphQL.Types
{
    public class FooInputType : InputObjectGraphType<FooInput>
    {
        public FooInputType(IModelData data)
        {
            Name = "FooInput";

            Field(o => o.Test, type: typeof(StringGraphType));
        }
    }
}
