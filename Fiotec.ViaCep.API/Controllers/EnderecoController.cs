using Fiotec.ViaCep.Application.Interfaces;
using Fiotec.ViaCep.Infra.Services.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Fiotec.ViaCep.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController(IEnderecoService enderecoService) : ControllerBase
    {
        /// <summary>
        /// Consulta endereço pelo CEP
        /// </summary>
        /// <param name="cep">CEP do endereço</param>
        /// <param name="ct">CancellationToken opcional</param>
        /// <returns>Endereço completo ou 204 se não encontrado</returns>
        [HttpGet("{cep}")]
        [ProducesResponseType(typeof(ViaCepResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ViaCepResponse>> ObterEnderecoPorCepAsync(string cep, CancellationToken ct = default)
        {
            var endereco = await enderecoService.ObterEnderecoPorCepAsync(cep, ct);
            return Ok(endereco);
        }
    }
}
