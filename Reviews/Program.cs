using Reviews.Domain;
using Reviews.GraphQl;

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
    .PublishSchemaDefinition(c => c.SetName("reviews"));

var app = builder.Build();

app.UseRouting();
app.UseWebSockets();
app.UseEndpoints(e => e.MapGraphQL());

await app.RunAsync();