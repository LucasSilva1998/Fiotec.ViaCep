using Fiotec.ViaCep.Infra.Services.Dto;

namespace Fiotec.ViaCep.Infra.Services.Interfaces
{
    public interface IViaCepService
    {
        Task<ViaCepResponse?> BuscarEnderecoPorCepAsync(string cep, CancellationToken ct = default);
    }
}