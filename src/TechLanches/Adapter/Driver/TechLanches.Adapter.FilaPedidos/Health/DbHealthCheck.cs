using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TechLanches.Adapter.SqlServer;

namespace TechLanches.Adapter.FilaPedidos.Health
{
    public class DbHealthCheck : IHealthCheck
    {
        private readonly TechLanchesDbContext _dbContext;

        public DbHealthCheck(TechLanchesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.Database.OpenConnectionAsync(cancellationToken);
                _dbContext.Database.CloseConnection();

                return HealthCheckResult.Healthy("Database connected with success.");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Error on connecting database.", ex);
            }
        }
    }
}