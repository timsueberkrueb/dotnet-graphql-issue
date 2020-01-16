using System;
using System.Collections.Generic;

using Example.GraphQL.Model;
using Example.GraphQL.Data;

namespace Example.GraphQL.Ext
{
    public class ModelData : IModelData
    {
        public string Whatever(string id)
        {
            return "whatever";
        }

        public string AddFoo(FooInput foo, BarInput bar, BazInput baz)
        {
            return "added foo";
        }

    }
}
