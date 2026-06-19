using CepHub.Models.DTOs;

namespace CepHub.Utils
{
    public class Logger
    {
        private readonly string _caminho;
        public Logger()
        {
            var directoryBase = Directory.GetCurrentDirectory();
            var dataDirectory = Path.Combine(directoryBase, "Data");

            Directory.CreateDirectory(dataDirectory);

            _caminho = Path.Combine(dataDirectory, "RegistroLogs.txt");
        }

        public void GravarLog(ViaCepDto endereco)
        {
            string log = $"[{DateTime.Now:dd-MM-yyyy HH:mm:ss}] " + $"{endereco.cep} | {endereco.logradouro}, {endereco.bairro}, {endereco.localidade} - {endereco.uf} " +
                                $"(Ibge: {endereco.ibge} | Gia: {endereco.gia} | Ddd: {endereco.ddd} | Siafi: {endereco.siafi})";

            using (StreamWriter streamWriter = new StreamWriter(_caminho, true))
            {
                streamWriter.WriteLine(log);       
            }
        }

        public List<string> ListarLogs()
        {
            var linhas = new List<string>();

            using (StreamReader streamReader = new StreamReader(_caminho))
            {
                while (!streamReader.EndOfStream)
                {
                    linhas.Add(streamReader.ReadLine());
                }
            }

            return linhas;
        }

    }
}