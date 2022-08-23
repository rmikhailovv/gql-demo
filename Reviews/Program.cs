using Reviews.Domain;
using Reviews.GraphQl;
using Reviews.GraphQl.Loaders;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddSingleton<Dictionary<Guid, Review>>();
services.AddSingleton<ReviewsAggregate>();

services.AddRouting();


services
    .AddGraphQLServer()
    .AddFiltering()
    .AddSorting()
    .AddQueryType<Query>()
    .AddMutationType<ReviewsAggregate>()
    .AddDataLoader<ProductsReviewsDataLoader>()
    .PublishSchemaDefinition(c => c.SetName("reviews"));

var app = builder.Build();

app.UseRouting();
app.UseWebSockets();
app.UseEndpoints(e => e.MapGraphQL());

await app.RunAsync();