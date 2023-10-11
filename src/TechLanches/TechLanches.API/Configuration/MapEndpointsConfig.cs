using Microsoft.OpenApi.Models;
using TechLanches.API.Endpoints;

namespace TechLanches.API.Configuration
{
    public static class MapEndpointsConfig
    {
        public static void UseMapEndpointsConfiguration(this IApplicationBuilder app)
        {
            //Precisa ajustar para pegar corretamente os maps dos endpoints.


            //if (app == null) throw new ArgumentNullException(nameof(app));

            //app.UseEndpoints(endpoints => {
            //    endpoints.MapPedidoEndpoints();
            //    endpoints.MapClienteEndpoints();
            //});
        }
    }
}
