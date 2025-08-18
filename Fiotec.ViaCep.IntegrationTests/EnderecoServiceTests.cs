using Fiotec.ViaCep.Application.Exceptions;
using Fiotec.ViaCep.Application.Services;
using Fiotec.ViaCep.Infra.Services.Dto;
using Fiotec.ViaCep.Infra.Services.Interfaces;
using Moq;

namespace Fiotec.ViaCep.IntegrationTests
{
    public class EnderecoServiceTests
    {
        private readonly Mock<IViaCepService> _viaCepServiceMock;
        private readonly EnderecoService _enderecoService;

        public EnderecoServiceTests()
        {
            _viaCepServiceMock = new Mock<IViaCepService>();
            _enderecoService = new EnderecoService(_viaCepServiceMock.Object);
        }

        [Fact]
        public async Task ObterEnderecoPorCepAsync_CepVazio_ThrowsParametroInvalidoException()
        {
            await Assert.ThrowsAsync<ParametroInvalidoException>(
                () => _enderecoService.ObterEnderecoPorCepAsync("", CancellationToken.None)
            );
        }

        [Fact]
        public async Task ObterEnderecoPorCepAsync_CepNaoEncontrado_ThrowsNaoEncontradoException()
        {
            _viaCepServiceMock
                .Setup(x => x.BuscarEnderecoPorCepAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((ViaCepResponse?)null);

            await Assert.ThrowsAsync<NaoEncontradoException>(
                () => _enderecoService.ObterEnderecoPorCepAsync("12345678", CancellationToken.None)
            );
        }

        [Fact]
        public async Task ObterEnderecoPorCepAsync_CepValido_RetornaEndereco()
        {
            var viaCepResponse = new ViaCepResponse
            {
                Cep = "12345678",
                Logradouro = "Rua Teste",
                Bairro = "Bairro Teste",
                Localidade = "Cidade Teste",
                Uf = "UF"
            };

            _viaCepServiceMock
                .Setup(x => x.BuscarEnderecoPorCepAsync("12345678", It.IsAny<CancellationToken>()))
                .ReturnsAsync(viaCepResponse);

            var result = await _enderecoService.ObterEnderecoPorCepAsync("12345678", CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("12345678", result.Cep);
        }
    }
}