using GraphQL.Types;

using Example.GraphQL.Model;
using Example.GraphQL.Data;

namespace Example.GraphQL.Types
{
    public class BazInputType : InputObjectGraphType<BazInput>
    {
        public BazInputType(IModelData data)
        {
            Name = "BazInput";

            Field(o => o.Test, type: typeof(StringGraphType));
        }
    }
}
