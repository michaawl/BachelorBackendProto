using GraphQlApi.Queries;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
        .ModifyRequestOptions(opt =>
        {
            opt.IncludeExceptionDetails = true;

        })
    .AddQueryType<Query>()
    .AddTypeExtension<TextQuery>()
    .AddTypeExtension<MediaQuery>()
    .AddTypeExtension<BlogQuery>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureEndpointDefaults(lo =>
        lo.Protocols = HttpProtocols.Http1AndHttp2);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();
ApiCache.Initialize();

app.UseCors("AllowFrontend");
app.MapGraphQL();

app.Run();
