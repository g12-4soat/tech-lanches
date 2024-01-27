using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using TechLanches.Adapter.ACL.Pagamento.QrCode.Constantes;
using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Adapter.ACL.Pagamento.QrCode.Models;

namespace TechLanches.Adapter.ACL.Pagamento.QrCode.Provedores.MercadoPago
{
    public class MercadoPagoService : IMercadoPagoService
    {
        private readonly HttpClient _httpClient;
        public MercadoPagoService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(MercadoPagoConstantes.MERCADO_PAGO);
        }

        public async Task<PagamentoResponseACLDTO> ConsultarPagamento(string idPagamentoComercial)
        {
           var response = await _httpClient.GetFromJsonAsync<Pedido>($"/merchant_orders/{idPagamentoComercial}");

            return new PagamentoResponseACLDTO()
            {
                PedidoId = Int32.Parse(response.ExternalReference),
                StatusPagamento = ConverterResultadoParaEnum(response.Pagamentos.LastOrDefault().Status)
            };
        }

        public async Task<string> GerarPagamentoEQrCode(string pagamentoMercadoPago, string usuarioId, string posId)
        {
            var content = new StringContent(pagamentoMercadoPago, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"instore/orders/qr/seller/collectors/{usuarioId}/pos/{posId}/qrs", content);

            string resultStr = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<PedidoGerado>(resultStr);

            return result.qr_data;
        }

        public Task<string> GerarPedidoEQrCode(string pedidoMercadoPago, string usuarioId, string posId)
        {
            throw new NotImplementedException();
        }

        private StatusPagamentoEnum ConverterResultadoParaEnum(string statusStr)
        {
            return statusStr.ToLower() switch
            {
                MercadoPagoConstantes.STATUS_APROVADO => StatusPagamentoEnum.Aprovado,
                _ => StatusPagamentoEnum.Reprovado,
            };
        }
    }
}
