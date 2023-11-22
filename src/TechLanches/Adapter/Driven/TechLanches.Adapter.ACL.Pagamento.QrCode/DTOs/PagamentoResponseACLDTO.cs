namespace TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs
{
    public class PagamentoResponseACLDTO
    {
        public StatusPagamentoEnum StatusPagamento { get; set;}
        public int PedidoId { get; set; }
    }
}