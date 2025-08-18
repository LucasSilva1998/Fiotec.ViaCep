using Fiotec.ViaCep.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Documentação do Swagger
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

// Swagger no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fiotec - ViaCEP API v1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
