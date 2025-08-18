using Fiotec.ViaCep.API.Extensions;
using Fiotec.ViaCep.Application.Extensions;
using Fiotec.ViaCep.Infra.Services.Interfaces;
using Fiotec.ViaCep.Infra.Services.Services;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

// Application Services 

builder.Services.AddApplicationServices();

// Configurações ViaCEP a partir do appsettings.json
var viaCepSettings = builder.Configuration.GetSection("ViaCepSettings");

// HttpClient para o serviço ViaCEP com Timeout e Retry 
builder.Services.AddHttpClient<IViaCepService, ViaCepService>(client =>
{
    client.BaseAddress = new Uri(viaCepSettings["BaseUrl"]);
    client.Timeout = TimeSpan.FromSeconds(int.Parse(viaCepSettings["TimeoutSeconds"]));
})
.AddPolicyHandler(HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(
        int.Parse(viaCepSettings["RetryCount"]),
        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
    ));

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
