using TechLanches.API.Endpoints;

namespace TechLanches.API.Configuration
{
    public static class MapEndpointsConfig
    {
        public static void UseMapEndpointsConfiguration(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapClienteEndpoints();

            endpoints.MapProdutoEndpoints();
        }
    }
}
