namespace TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs
{
    public record PagamentoResponseACLDTO
    {
        public StatusPagamentoEnum StatusPagamento { get; set;}
        public int PedidoId { get; set; }
    }
}