using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CepHub.Utils
{
    public class CepNormalizer
    {
        public string Normalizar(string cep)
        { 
            if (string.IsNullOrEmpty(cep))
                throw new ArgumentException("O CEP não pode ser nulo, conter espacos em branco ou vazio.");
            
            string cepFormatado = Regex.Replace(cep, @"\D", ""); // Remove caracteres não numéricos

            if(cepFormatado.Length != 8)
                throw new ArgumentException("O CEP deve conter exatamente 8 dígitos numéricos.");

            return cepFormatado;
        } 
    }
}