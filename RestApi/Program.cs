using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureEndpointDefaults(lo =>
        lo.Protocols = HttpProtocols.Http1AndHttp2);
});


builder.Services.AddControllers();

var app = builder.Build();
ApiCache.Initialize();

app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();
app.Run();
