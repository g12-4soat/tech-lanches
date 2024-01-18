namespace TechLanches.Adapter.RabbitMq.Messaging
{
    public interface IRabbitMqService
    {
        void Publicar(int data);
        Task Consumir(Func<string, Task> function);
    }
}
