using GrpcWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddGrpc(options =>
{
    options.MaxReceiveMessageSize = 200 * 1024 * 1024;
    options.MaxSendMessageSize = 200 * 1024 * 1024;
});

var app = builder.Build();
ApiCache.Initialize();

app.UseCors();
app.UseGrpcWeb();

app.MapGrpcService<TextService>().EnableGrpcWeb().RequireCors();
app.MapGrpcService<MediaService>().EnableGrpcWeb().RequireCors();
app.MapGrpcService<BlogService>().EnableGrpcWeb().RequireCors();

app.MapGet("/test-cors", () => "CORS works!").RequireCors();

app.MapGet("/", () => "GRPC server is running.");

app.Run();
