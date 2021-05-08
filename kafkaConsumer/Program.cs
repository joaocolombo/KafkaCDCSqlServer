using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;

class Program
{
    public static async Task Main(string[] args)
    {

        var taskSkuLojista = new ConsumerTopico("origem.dbo.SkuLojista");
        var taskLojista = new ConsumerTopico("origem.dbo.Lojista");
        var taskSku = new ConsumerTopico("origem.dbo.Produto");
        var taskProduto = new ConsumerTopico("origem.dbo.Sku");

        await Task.WhenAll(new[] {taskProduto.Run(), taskLojista.Run(), taskSku.Run(), taskSkuLojista.Run()});
    }


    public class ConsumerTopico
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
                        Console.WriteLine($"------------------------{Topico}-----------------------------");

                        Console.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error occured: {e.Error.Reason}");
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

