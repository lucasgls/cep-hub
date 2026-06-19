using CepHub.Models.DTOs;
using CepHub.Services;
using CepHub.Utils;
using CepHub.View;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetCepz(string cep)
        {
            try
            {
                var cepData = await _cepService.ConsultarCepAsync(cep);

                return Ok(new
                {
                    cep = cepData.Cep,
                    logradouro = cepData.Logradouro,
                    complemento = cepData.Complemento,
                    bairro = cepData.Bairro,
                    localidade = cepData.Localidade,
                    uf = cepData.Uf
                });
            }
            catch (HttpRequestException)
            {
                return StatusCode(503, new
                {
                    message = "Serviço indisponível.",
                    details = "Erro ao acessar a API de CEP."
                });
            }
            catch (ArgumentException)
            {
                return StatusCode(400, new
                {
                    message = "Requisição inválida.",
                    details = "O CEP informado é inválido."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "Erro interno.",
                    details = "Ocorreu um erro inesperado."
                });
            }
        }

        [HttpGet("logs")]
        public IActionResult GetLogs()
        {
            try
            {
                var logs = _logger.ListarLogs();

                if (logs == null)
                    return NotFound("Nenhum log encontrado.");

                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar logs: {ex.Message}");
            }
        }

    }
}