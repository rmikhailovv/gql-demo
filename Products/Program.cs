using Products.Domain;
using Products.GraphQl;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddSingleton<Dictionary<Guid, Product>>();
services.AddSingleton<ProductsAggregate>();

services.AddRouting();

services
    .AddGraphQLServer()
    .AddFiltering()
    .AddSorting()
    .AddQueryType<Query>()
    .AddMutationType<ProductsAggregate>()
    .PublishSchemaDefinition(c => c
        .SetName("products")
        .AddTypeExtensionsFromFile("Schema.graphql"));

var app = builder.Build();

app.UseRouting();
app.UseWebSockets();
app.UseEndpoints(e => e.MapGraphQL());

await app.RunAsync();
