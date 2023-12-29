using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;

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

            var connectionFactory = new ConnectionFactory { HostName = _rabbitHost, UserName = _rabbitUser, Password = _rabbitPassword };
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _rabbitQueueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public void Publicar(int data)
        {
            var mensagem = Encoding.UTF8.GetBytes(data.ToString());

            _channel.BasicPublish(exchange: string.Empty,
                                  routingKey: _rabbitQueueName,
                                  null,
                                  body: mensagem);
        }
    }
}
