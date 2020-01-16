using GraphQL.Types;

using Example.GraphQL.Model;
using Example.GraphQL.Data;

namespace Example.GraphQL.Types
{
    public class BarInputType : InputObjectGraphType<BarInput>
    {
        public BarInputType(IModelData data)
        {
            Name = "BarInput";

            Field(o => o.Test, type: typeof(StringGraphType));
        }
    }
}
