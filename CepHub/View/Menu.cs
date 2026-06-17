using CepHub.Models;
using CepHub.Models.DTOs;
using CepHub.Services;
using CepHub.Utils;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CepHub.View
{
    public class Menu
    {
        // Simulando injeção de dependência
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly Logger logger = new Logger();
        private static readonly CepNormalizer cepNormalizer = new CepNormalizer();

        private static readonly CepService cepService = new CepService(httpClient, logger, cepNormalizer);

        public async Task Run()
        {
            while (true)
            {
                ExibirMenu();

                string opcao = Console.ReadLine();

                if (opcao == "0")
                    break;

                switch (opcao)
                {
                    case "1":
                        await ConsultarCep();
                        break;

                    case "2":
                        ListarEnderecos();
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                Console.WriteLine("\nPressione ENTER para continuar...");
                Console.ReadLine();
            }

            Console.WriteLine("Programa encerrado.");
        }

        private async Task ConsultarCep()
        {
            Console.Write("Digite o CEP: ");
            string? cep = Console.ReadLine();

            try
            {
                var endereco = await cepService.ConsultarCepAsync(cep);
                ExibirEndereco(endereco);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        private void ExibirEndereco(ResponseEnderecoDto endereco)
        {
            Console.WriteLine("\n|Resultado:");
            Console.WriteLine($"|CEP: {endereco.Cep}");
            Console.WriteLine($"|Logradouro: {endereco.Logradouro}");
            Console.WriteLine($"|Bairro: {endereco.Bairro}");
            Console.WriteLine($"|Cidade: {endereco.Localidade}");
            Console.WriteLine($"|UF: {endereco.Uf}\n");
        }

        private void ExibirMenu()
        {
            Console.Clear();
            Console.WriteLine("|Menu CEP:");
            Console.WriteLine("|1. Consultar CEP");
            Console.WriteLine("|2. Listar Logs de Consulta");
            Console.WriteLine("|0. Sair\n");
            Console.Write("Opção: ");
        }

        private void ListarEnderecos()
        {
            Console.WriteLine("\nEndereços consultados:");
            logger.ListarLogs();
        }
    }
}