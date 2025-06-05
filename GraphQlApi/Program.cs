using GraphQlApi.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()             
    .AddTypeExtension<TextQuery>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // <-- Update to match your frontend port
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();
app.MapGraphQL();
app.UseCors("AllowFrontend");
app.Run();
