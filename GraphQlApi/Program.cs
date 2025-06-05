using GraphQlApi.Queries;

var builder = WebApplication.CreateBuilder(args);

// 1) Add GraphQL / HotChocolate
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

// 2) Add CORS and allow your frontend origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // your React/Vite origin
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// 3) Use CORS before MapGraphQL
app.UseCors("AllowFrontend");

// 4) Finally map GraphQL endpoint at “/graphql”
app.MapGraphQL();

app.Run();
