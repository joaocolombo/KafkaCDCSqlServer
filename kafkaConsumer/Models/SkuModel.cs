using System.Text.Json;

namespace kafkaConsumer.Models
{
    public class SkuModel 
    {
            public int IdSku { get; set; }
            public string Nome { get; set; }
            public int IdProduto { get; set; }
            public string Cor { get; set; }
            public string Voltagem { get; set; }
            public override string ToString()
            {
                return JsonSerializer.Serialize(this);
            }
    }
}