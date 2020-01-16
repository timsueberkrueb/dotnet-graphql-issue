using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using GraphQL;
using GraphQL.Server;
using GraphQL.Types;
using GraphQL.Utilities;
using GraphQL.Server.Ui.Playground;
using GraphQL.Server.Ui.GraphiQL;

using Example.GraphQL.Model;
using Example.GraphQL.Data;
using Example.GraphQL.Types;
using Example.GraphQL.Ext;

namespace backend
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // FIXME: See https://github.com/graphql-dotnet/graphql-dotnet/issues/1116
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // FIXME: See https://github.com/graphql-dotnet/graphql-dotnet/issues/1161#issuecomment-540306485
            services.AddScoped<IDependencyResolver>(
                provider => new FuncDependencyResolver(provider.GetRequiredService));
            services.AddScoped<ISchema, GraphQLSchema>();

            services.AddSingleton<IModelData, ModelData>();
            services.AddSingleton<QueryType>();
            services.AddSingleton<MutationType>();

            GraphTypeTypeRegistry.Register(typeof(FooInput), typeof(FooInputType));
            GraphTypeTypeRegistry.Register(typeof(Mutation), typeof(MutationType));
            GraphTypeTypeRegistry.Register(typeof(Query), typeof(QueryType));

            services.AddGraphQL(opts =>
            {
                opts.EnableMetrics = true;
                opts.ExposeExceptions = true;
            }).AddGraphTypes(ServiceLifetime.Scoped);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env, ISchema schema)
        {
            var printedSchema = new SchemaPrinter(schema).Print();
            Console.WriteLine(printedSchema);

            Console.WriteLine(schema.FindType("FooInput"));
            Console.WriteLine(schema.FindType("BarInput"));
            Console.WriteLine(schema.FindType("BazInput"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHsts();
            }

            // add http for Schema at default url /graphql
            app.UseGraphQL<ISchema>("/graphql");
            app.UseWebSockets();
            app.UseGraphQLWebSockets<ISchema>("/graphql");

            // use graphql-playground at default url /ui/playground
            app.UseGraphiQLServer(new GraphiQLOptions { GraphiQLPath = "/graphiql", GraphQLEndPoint = "/graphql" });
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
