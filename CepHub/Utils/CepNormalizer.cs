using System.Text.RegularExpressions;

namespace CepHub.Utils
{
    public class CepNormalizer
    {
        public string Normalizar(string cep)
        { 
            if (string.IsNullOrEmpty(cep))
                throw new ArgumentException("O CEP não pode ser nulo, conter espacos em branco ou vazio.");
            
            string cepFormatado = Regex.Replace(cep, @"\D", ""); 

            if(cepFormatado.Length != 8)
                throw new ArgumentException("O CEP deve conter exatamente 8 dígitos numéricos.");

            return cepFormatado;
        } 
    }
}