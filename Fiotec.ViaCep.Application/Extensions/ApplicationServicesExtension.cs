using Fiotec.ViaCep.Application.Interfaces;
using Fiotec.ViaCep.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fiotec.ViaCep.Application.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)

        {
            services.AddScoped<IEnderecoService, EnderecoService>();

            return services;
        }
    }
}