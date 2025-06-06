using Grpc.AspNetCore.Web;
using GrpcWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

// CORS konfigurieren
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5109, listenOptions =>
    {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
    });
});


var app = builder.Build();

app.UseCors();
app.UseGrpcWeb();

app.MapGrpcService<TextService>().EnableGrpcWeb().RequireCors();
app.MapGrpcService<MediaService>().EnableGrpcWeb().RequireCors();
app.MapGrpcService<BlogService>().EnableGrpcWeb().RequireCors();

app.MapGet("/test-cors", () => "CORS works!").RequireCors();

app.MapGet("/", () => "This server is running. Use a gRPC client to communicate with gRPC endpoints.");

app.Run();
