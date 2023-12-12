using Microsoft.Extensions.Configuration;

namespace TechLanches.Application
{
    public static class AppSettings
    {
        public static IConfiguration Configuration;
        public static bool IsDevelopment() => new AppConfiguration().ENV == "Dev";
        public static bool IsProduction() => new AppConfiguration().ENV == "Prod";
        public static bool IsTest() => new AppConfiguration().ENV == "Test";
        public static string GetEnv() => new AppConfiguration().ENV;
    }

    public class AppConfiguration
    {
        public string ENV 
        { 
            get => AppSettings.Configuration.GetConnectionString("DeployedEnvironment"); 
        }
    }
}
