using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using TechLanches.Adapter.ACL.Pagamento.QrCode.Configuration;
using TechLanches.Adapter.ACL.Pagamento.QrCode.Models;

namespace TechLanches.Adapter.ACL.Pagamento.QrCode.Provedores.MercadoPago
{
    public class MercadoPagoService : IMercadoPagoService
    {
        private readonly HttpClient _httpClient;
        private const string merchant_orders = "1d500ba2-ae69-442f-9f30-ccf22955b11f";

        public MercadoPagoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PedidoComercial> ObterPedido(string pedidoComercial)
            => await _httpClient.GetFromJsonAsync<PedidoComercial>($"/merchant_orders/{pedidoComercial}");
    }
}
