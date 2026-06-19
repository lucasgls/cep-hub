using CepHub.Models;
using CepHub.Models.DTOs;
using CepHub.Utils;
using System.Text.Json;

namespace CepHub.Services
{
    public class CepService
    {
        private const string UrlViaCep = "https://viacep.com.br/ws/{0}/json/";

        private readonly HttpClient _httpClient;
        private readonly Logger _logger;
        private readonly CepNormalizer _normalizer;

        public CepService(HttpClient httpClient, Logger loggerService, CepNormalizer normalizer)
        {
            _httpClient = httpClient;
            _logger = loggerService;
            _normalizer = normalizer;
        }

        public async Task<ResponseEnderecoDto> ConsultarCepAsync(string cep)
        {
            var cepFormatado = _normalizer.Normalizar(cep);
            var url = string.Format(UrlViaCep, cepFormatado);

            var json = await ObterJsonAsync(url); 

            var endereco = Desserializar(json); 

            _logger.GravarLog(endereco); 

            return Tranformador(endereco);
        }
        
        private async Task<string> ObterJsonAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);

                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException("Erro ao consultar CEP: " + e);
            }
            catch (TaskCanceledException e)
            {
                throw new Exception("A requisição para consultar o CEP foi cancelada: " + e);
            }
        }

        public void ValidarCepExistente(ViaCepDto endereco)
        {
            if (endereco == null)
                throw new ArgumentException("Resposta inválida.");

            if (!string.IsNullOrEmpty(endereco.erro) && endereco.erro.ToLower() == "true")
                throw new ArgumentException("CEP inválido.");
            
        }

        private ViaCepDto Desserializar(string json)
        {
            ViaCepDto endereco;
            try
            {
                endereco = JsonSerializer.Deserialize<ViaCepDto>(json);
            }
            catch (JsonException e)
            {
                throw new JsonException("Erro ao desserializar JSON: " + e.Message);
            }

            ValidarCepExistente(endereco);

            return endereco;
        }

        private ResponseEnderecoDto Tranformador(ViaCepDto endereco)
        {
            return new ResponseEnderecoDto
            {
                Cep = endereco.cep,
                Logradouro = endereco.logradouro,
                Bairro = endereco.bairro,
                Localidade = endereco.localidade,
                Uf = endereco.uf
            };
        }
    }
}