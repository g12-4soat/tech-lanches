using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace TechLanches.Adapter.RabbitMq.Messaging
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _rabbitHost;
        private readonly string _rabbitUser;
        private readonly string _rabbitPassword;
        private readonly string _rabbitQueueName;

        public RabbitMqService(IConfiguration configuration)
        {
            _rabbitHost = configuration.GetSection("RabbitMQ:Host").Value;
            _rabbitUser = configuration.GetSection("RabbitMQ:User").Value;
            _rabbitPassword = configuration.GetSection("RabbitMQ:Password").Value;
            _rabbitQueueName = configuration.GetSection("RabbitMQ:Queue").Value;

            var connectionFactory = new ConnectionFactory { HostName = _rabbitHost, UserName = _rabbitUser, Password = _rabbitPassword, DispatchConsumersAsync = true };
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _rabbitQueueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public async Task Consumir(Func<string, Task> function)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {

                var body = ea.Body.ToArray();
                await function(Encoding.UTF8.GetString(body));
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(queue: _rabbitQueueName, autoAck: false, consumer: consumer);
        }

        public void Publicar(int data)
        {
            var mensagem = Encoding.UTF8.GetBytes(data.ToString());

            var properties = _channel.CreateBasicProperties();
            properties.DeliveryMode = 2; // Marca a mensagem como persistente
            _channel.BasicPublish(exchange: string.Empty,
                                  routingKey: _rabbitQueueName,
                                  basicProperties: properties,
                                  body: mensagem);
        }
    }
}
