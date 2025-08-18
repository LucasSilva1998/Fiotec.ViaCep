using Microsoft.OpenApi.Models;

namespace Fiotec.ViaCep.API.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Fiotec - ViaCEP API - Consulta de Endereços",
                    Version = "v1",
                    Description = "API responsável por consultar endereços a partir do CEP utilizando o serviço ViaCEP.",
                    Contact = new OpenApiContact
                    {
                        Name = "Lucas Pereira",
                        Email = "lucassilva@fiotec.fiocruz.com",
                        Url = new Uri("https://github.com/LucasSilva1998/fiotec-viaCep")
                    }
                });

            });

            return services;
        }
    }
}

