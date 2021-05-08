using System.Text.Json;

namespace kafkaConsumer.Models
{
    public class SkuLojistaModel
    {
        public int IdSku { get; set; }
        public int IdLojista { get; set; }
        public float PrecoVenda { get; set; }
        public float PrecoAnterior { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}