var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddHttpClient("products", (sp, client) =>
{
    client.BaseAddress = new Uri("http://localhost:5001/graphql");
});
services.AddHttpClient("reviews", (sp, client) =>
{
    client.BaseAddress = new Uri("http://localhost:5002/graphql");
});

services
    .AddGraphQLServer()
    .AddRemoteSchema("products")
    .AddRemoteSchema("reviews");

var app = builder.Build();

app.UseRouting();
app.UseWebSockets();
app.UseEndpoints(e => e.MapGraphQL());

await app.RunAsync();
