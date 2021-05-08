using System.Text.Json;

namespace kafkaConsumer.Models
{
    public class LojistaModel
    {
        public int IdLojista { get; set; }
        public string Nome { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}