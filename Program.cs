var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Invoice API",
        Version = "v1",
        Description = "API for fetching invoice data"
    });
});

var app = builder.Build();

// Swagger UI available at /swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoice API v1");
});

app.UseDefaultFiles();   // serves wwwroot/index.html at "/"
app.UseStaticFiles();    // serves wwwroot/styles.css, script.js, etc.

app.UseRouting();
app.MapControllers();

app.Run();
