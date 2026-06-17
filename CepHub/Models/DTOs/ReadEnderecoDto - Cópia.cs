namespace CepHub.Models.DTOs
{
    public class ReadEnderecoDto
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }

        public override string ToString()
        {
            return $"{this.Cep} | {this.Logradouro}, {this.Bairro}, {this.Localidade} - {this.Uf}";
        }
    }
}