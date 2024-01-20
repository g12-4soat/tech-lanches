using TechLanches.Adapter.ACL.Pagamento.QrCode.Constantes;
using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;

namespace TechLanches.Adapter.ACL.Pagamento.QrCode.Provedores.MercadoPago
{
    public class MercadoPagoMockadoService : IMercadoPagoMockadoService
    {
        public Task<PagamentoResponseACLDTO> ConsultarPagamento(string idPagamentoComercial)
        {
            string randomStr = ObterStatusPagamentoSimulado();
            var consultaPagamentoResponse = new PagamentoResponseACLDTO
            {
                StatusPagamento = ConverterResultadoParaEnum(randomStr),
                PedidoId = 0
            };

            return Task.FromResult(consultaPagamentoResponse);
        }

        public Task<string> GerarPagamentoEQrCode(string pedidoMercadoPago, string usuarioId, string posId)
        {
            return Task.FromResult("00020101021226940014BR.GOV.BCB.PIX2572pix-qr-h.mercadopago.com/instore/h/p/v2/968f23cc18f946678a366d10d883809a43530016com.mercadolibre0129https://mpago.la/pos/889704125204000053039865802BR5909Test Test6009SAO PAULO62070503***6304091A");
        }

        private string ObterStatusPagamentoSimulado()
        {
            Random random = new Random();
            string[] statuses = { MercadoPagoConstantes.STATUS_APROVADO, MercadoPagoConstantes.STATUS_REPROVADO };
            return statuses[random.Next(statuses.Length)];
        }

        private StatusPagamentoEnum ConverterResultadoParaEnum(string statusStr)
        {
            return statusStr.ToLower() switch
            {
                MercadoPagoConstantes.STATUS_APROVADO => StatusPagamentoEnum.Aprovado,
                MercadoPagoConstantes.STATUS_REPROVADO => StatusPagamentoEnum.Reprovado,
                _ => throw new ArgumentException("String de status pagamento inválida"),
            };
        }
    }
}