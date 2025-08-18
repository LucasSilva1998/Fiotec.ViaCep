using Fiotec.ViaCep.Application.Exceptions;
using Fiotec.ViaCep.Application.Interfaces;
using Fiotec.ViaCep.Infra.Services.Dto;
using Fiotec.ViaCep.Infra.Services.Interfaces;

namespace Fiotec.ViaCep.Application.Services
{
    public class EnderecoService(IViaCepService viaCepService) : IEnderecoService
    {
        public async Task<ViaCepResponse> ObterEnderecoPorCepAsync(string cep, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(cep))
                throw new ParametroInvalidoException("O CEP não pode ser vazio.");

            var endereco = await viaCepService.BuscarEnderecoPorCepAsync(cep, ct) ??
                throw new NaoEncontradoException($"CEP {cep} não encontrado");

            return endereco;
        }
    }
}