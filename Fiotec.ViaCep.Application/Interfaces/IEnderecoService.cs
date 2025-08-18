using Fiotec.ViaCep.Infra.Services.Dto;

namespace Fiotec.ViaCep.Application.Interfaces
{
    public interface IEnderecoService
    {
        Task<ViaCepResponse?> ObterEnderecoPorCepAsync(string cep, CancellationToken ct = default);
    }
}