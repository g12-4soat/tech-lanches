using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechLanches.Adapter.RabbitMq.Messaging
{
    public interface IRabbitMqService
    {
        void Publicar(int data);
        Task Consumir(Func<Task> function);
    }
}
