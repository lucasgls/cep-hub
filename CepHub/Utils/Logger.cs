using CepHub.Models.DTOs;

namespace CepHub.Utils
{
    public class Logger
    {
        private readonly string _caminho;
        public Logger()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var dataDirectory = Path.Combine(baseDirectory, "Data");

            Directory.CreateDirectory(dataDirectory); 

            _caminho = Path.Combine(dataDirectory, "RegistroLogs.txt");
        }

        public void GravarLog(ViaCepDto endereco)
        {
            string log = $"[{DateTime.Now:dd-MM-yyyy HH:mm:ss}] " + $"{endereco.cep} | {endereco.logradouro}, {endereco.bairro}, {endereco.localidade} - {endereco.uf} " +
                                $"(Ibge: {endereco.ibge} | Gia: {endereco.gia} | Ddd: {endereco.ddd} | Siafi: {endereco .siafi})";


            using (StreamWriter streamWriter = new StreamWriter(_caminho, true))
            {
                streamWriter.WriteLine(log);       
            }
        }

        public void ListarLogs()
        {
            using(StreamReader streamReader = new StreamReader(_caminho))
            {
                while(!streamReader.EndOfStream)
                {
                    Console.WriteLine(streamReader.ReadLine());       
                }
            }
        }
    }
}