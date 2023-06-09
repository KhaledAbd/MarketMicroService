using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot().AddCacheManager(d =>
{
    d.WithDictionaryHandle();
});

builder.Host.ConfigureAppConfiguration((hosingContext, config) =>
{
    config.AddJsonFile($"ocelot.{hosingContext.HostingEnvironment.EnvironmentName}.json", true, true);
}).ConfigureLogging((hostingContext, loggingBuilder) =>
{
    loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});
var app = builder.Build();

await app.UseOcelot();
app.Run();
