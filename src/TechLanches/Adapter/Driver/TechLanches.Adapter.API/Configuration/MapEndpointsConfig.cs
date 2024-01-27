using TechLanches.Adapter.API.Endpoints;

namespace TechLanches.Adapter.API.Configuration
{
    public static class MapEndpointsConfig
    {
        public static void UseMapEndpointsConfiguration(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapClienteEndpoints();
            endpoints.MapPedidoEndpoints();
            endpoints.MapProdutoEndpoints();
            endpoints.MapFilaPedidoEndpoints();
            endpoints.MapPagamentoEndpoints();
            endpoints.MapCheckoutEndpoints();
        }
    }
}
