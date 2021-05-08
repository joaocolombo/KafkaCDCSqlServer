using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using kafkaConsumer.Models;

class Program
{
    public static async Task Main(string[] args)
    {

        var taskSkuLojista = new ConsumerTopico<SkuLojistaModel>("origem.dbo.SkuLojista");
        var taskLojista = new ConsumerTopico<LojistaModel>("origem.dbo.Lojista");
        var taskProduto= new ConsumerTopico<ProdutoModel>("origem.dbo.Produto");
        var taskSku = new ConsumerTopico<SkuModel>("origem.dbo.Sku");

        await Task.WhenAll(new[] {taskProduto.Run(), taskLojista.Run(), taskSku.Run(), taskSkuLojista.Run()});
    }


    public class ConsumerTopico<T>
    {
        public ConsumerTopico(string topico)
        {
            Topico = topico;
        }

        private string Topico { get; }
        public async Task Run()
        {
            var conf = new ConsumerConfig
            {
                GroupId = "consumer1",
                BootstrapServers = "kafka",
                AutoOffsetReset = AutoOffsetReset.Earliest

            };

            using var c = new ConsumerBuilder<Ignore, string>(conf).Build();

            c.Subscribe(Topico);

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                while (true)
                {
                    try
                    {
                        await Task.Delay(10, cts.Token);
                        var cr = c.Consume(cts.Token);
                        if (cr.Message?.Value == null)
                            continue;

                        Console.WriteLine($"------------------------{Topico}-----------------------------");
    
                        var mensagem = JsonSerializer.Deserialize<BaseModel<T>>(cr.Message.Value);

                        var before = mensagem.payload.before == null ? "Null" : mensagem.payload.before.ToString();
                        var after= mensagem.payload.after == null ? "Null" : mensagem.payload.after.ToString();
                        Console.WriteLine(
                            $"Executado: {mensagem.payload.op} | {before} => {after}");
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error occured: {e.Error.Reason}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error occured: {e.Message}");
                    }
                }
            }
            catch (OperationCanceledException e)
            {
                c.Close();
            }

        }
    }
}

