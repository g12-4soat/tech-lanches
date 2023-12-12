using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;

namespace TechLanches.Adapter.ACL.Pagamento.QrCode.Provedores.MercadoPago
{
    public class MercadoPagoMocadoService : IPagamentoQrCodeACLService
    {
        public Task<PagamentoResponseACLDTO> ConsultarPagamento(int mercadoPagoVendaId)
        {
            //simula chamada http
            string randomStr = ObterStatusPagamentoSimulado();
            var consultaPagamentoResponse = new PagamentoResponseACLDTO
            {
                StatusPagamento = ConverterResultadoParaEnum(randomStr),
                PedidoId = 0
            };

            return Task.FromResult(consultaPagamentoResponse);
        }

        public Task<string> GerarQrCode(string url)
        {
            return Task.FromResult("qrcodedata");
        }

        private string ObterStatusPagamentoSimulado()
        {
            Random random = new Random();
            string[] statuses = { "approved", "repproved" };
            return statuses[random.Next(statuses.Length)];
        }

        private StatusPagamentoEnum ConverterResultadoParaEnum(string statusStr)
        {
            return statusStr.ToLower() switch
            {
                "approved" => StatusPagamentoEnum.Aprovado,
                "repproved" => StatusPagamentoEnum.Reprovado,
                _ => throw new ArgumentException("String de status pagamento inválida"),
            };
        }

    }
}