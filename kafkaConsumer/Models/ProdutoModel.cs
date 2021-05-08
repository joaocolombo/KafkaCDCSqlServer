using System.Text.Json;

namespace kafkaConsumer.Models
{
    public class ProdutoModel
    {
        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}