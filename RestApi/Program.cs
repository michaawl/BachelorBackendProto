var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // <-- Update to match your frontend port
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Use CORS BEFORE any authorization/middleware that handles requests
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
