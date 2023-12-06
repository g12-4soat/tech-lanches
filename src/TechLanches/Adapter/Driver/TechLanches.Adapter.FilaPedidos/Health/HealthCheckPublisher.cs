using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TechLanches.Adapter.FilaPedidos.Health
{
    public class HealthCheckPublisher : IHealthCheckPublisher
    {
        private readonly string _fileName;
        private HealthStatus _prevStatus = HealthStatus.Unhealthy;

        public HealthCheckPublisher(IConfiguration _configuration)
        {
            _fileName = _configuration["HealthCheck:FilePath"];
        }

        public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            var fileExists = _prevStatus == HealthStatus.Healthy;

            if (report.Status == HealthStatus.Healthy)
            {
                using var _ = File.Create(_fileName);
            }
            else if (fileExists)
            {
                File.Delete(_fileName);
            }

            _prevStatus = report.Status;
            return Task.CompletedTask;
        }
    }
}
