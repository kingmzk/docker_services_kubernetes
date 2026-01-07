using Mango.GatewaySolution.Extentions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;



var builder = WebApplication.CreateBuilder(args);

builder.AddAppAuthentication();
builder.Configuration.AddJsonFile("./ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddJsonFile("ocelot/ocelot.docker.json", optional: false);

var app = builder.Build();



app.MapGet("/", () => "Hello World!");
app.UseOcelot().GetAwaiter().GetResult();
app.Run();
