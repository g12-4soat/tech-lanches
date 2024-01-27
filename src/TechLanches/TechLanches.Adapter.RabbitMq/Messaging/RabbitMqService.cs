using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using TechLanches.Adapter.RabbitMq.Options;

namespace TechLanches.Adapter.RabbitMq.Messaging
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly RabbitOptions _rabbitOptions;

        public RabbitMqService(IOptions<RabbitOptions> rabbitOptions)
        {
            _rabbitOptions = rabbitOptions.Value;

            var connectionFactory = new ConnectionFactory { HostName = _rabbitOptions.Host, UserName = _rabbitOptions.User, Password = _rabbitOptions.Password, DispatchConsumersAsync = true };
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _rabbitOptions.Queue,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
            
            _channel.BasicQos(0, 1, false);
        }

        public async Task Consumir(Func<string, Task> function)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    await function(Encoding.UTF8.GetString(body));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
                }
                finally 
                {
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
            };

            _channel.BasicConsume(queue: _rabbitOptions.Queue, autoAck: false, consumer: consumer);
        }

        public void Publicar(int data)
        {
            var mensagem = Encoding.UTF8.GetBytes(data.ToString());

            var properties = _channel.CreateBasicProperties();
            properties.DeliveryMode = 2; // Marca a mensagem como persistente
            _channel.BasicPublish(exchange: string.Empty,
                                  routingKey: _rabbitOptions.Queue,
                                  basicProperties: properties,
                                  body: mensagem);
        }
    }
}
