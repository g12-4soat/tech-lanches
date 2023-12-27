using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TechLanches.Adapter.RabbitMq.Messaging
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string QUEUENAME = "pedidos";

        public RabbitMqService()
        {
            var connectionFactory = new ConnectionFactory { HostName = "localhost", UserName = "admin", Password = "123456" };
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: QUEUENAME,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public void Publicar(int data)
        {
            var mensagem = Encoding.UTF8.GetBytes(data.ToString());

            _channel.BasicPublish(exchange: string.Empty,
                                  routingKey: QUEUENAME,
                                  null,
                                  body: mensagem);
        }
    }
}
