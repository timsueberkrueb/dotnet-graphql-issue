using System;
using System.Collections.Generic;

using Example.GraphQL.Model;

namespace Example.GraphQL.Data
{
    public interface IModelData
    {
        string Whatever(string id);

        string AddFoo(FooInput foo, BarInput bar, BazInput baz);
    }
}
