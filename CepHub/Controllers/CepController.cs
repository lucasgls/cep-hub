using CepHub.Services;
using CepHub.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CepHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CepController : ControllerBase
    {
        private readonly CepService _cepService;
        private readonly Logger _logger;

        public CepController(CepService cepService, Logger logger)
        {
            _cepService = cepService;
            _logger = logger;
        }

        /// <summary>
        /// Consulta o endereço de um CEP.
        /// </summary>
        /// <param name="cep">CEP a ser consultado. Pode conter máscara (ex: 01310-100) ou apenas dígitos (ex: 01310100).</param>
        /// <response code="200">Endereço encontrado com sucesso.</response>
        /// <response code="400">CEP inválido ou mal formatado.</response>
        /// <response code="503">Serviço de CEP externo indisponível.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpGet("{cep}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCep(string cep)
        {
            try
            {
                var cepData = await _cepService.ConsultarCepAsync(cep);
                return Ok(cepData);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new
                {
                    message = "Serviço indisponível."
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "Erro interno."
                });
            }
        }

        /// <summary>
        /// Retorna todos os logs de consultas realizadas.
        /// </summary>
        /// <response code="200">Logs retornados com sucesso.</response>
        /// <response code="204">Nenhum log encontrado.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpGet("logs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetLogs()
        {
            try
            {
                var logs = _logger.ListarLogs();

                if (logs.Count == 0)
                    return NoContent();

                return Ok(logs);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "Erro interno."
                });
            }
        }
    }
}