using CepHub.Services;
using CepHub.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CepHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CepController : ControllerBase
    {
        private readonly CepService _cepService;
        private readonly Logger _logger;

        public CepController(CepService cepService, Logger logger)
        {
            _cepService = cepService;
            _logger = logger;
        }

        [HttpGet("{cep}")]
        public async Task<IActionResult> GetCep(string cep)
        {
            try
            {
                var cepData = await _cepService.ConsultarCepAsync(cep);
                return Ok(cepData);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
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

        [HttpGet("logs")]
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