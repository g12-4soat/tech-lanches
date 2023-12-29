using Microsoft.Extensions.Diagnostics.HealthChecks;
using RabbitMQ.Client;

namespace TechLanches.Adapter.FilaPedidos.Health
{
    public class RabbitMQHealthCheck : IHealthCheck
    {
        private readonly string _rabbitHost;
        private readonly string _rabbitUser;
        private readonly string _rabbitPassword;
        private readonly string _rabbitQueueName;

        public RabbitMQHealthCheck(IConfiguration configuration)
        {
            _rabbitHost = configuration.GetSection("RabbitMQ:Host").Value;
            _rabbitUser = configuration.GetSection("RabbitMQ:User").Value;
            _rabbitPassword = configuration.GetSection("RabbitMQ:Password").Value;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var factory = new ConnectionFactory { HostName = _rabbitHost, UserName = _rabbitUser, Password = _rabbitPassword };

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    // Verifica se a conexão e o canal com o RabbitMQ podem ser estabelecidos
                    if (connection.IsOpen && channel.IsOpen)
                    {
                        return HealthCheckResult.Healthy("RabbitMQ is reachable.");
                    }
                    else
                    {
                        return HealthCheckResult.Unhealthy("Unable to connect to RabbitMQ.");
                    }
                }
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"Exception during RabbitMQ health check: {ex.Message}");
            }
        }
    }
}
