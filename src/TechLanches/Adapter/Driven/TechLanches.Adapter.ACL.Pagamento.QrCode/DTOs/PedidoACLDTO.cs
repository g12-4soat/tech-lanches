namespace TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs
{
    public record PedidoACLDTO
    {
        public decimal Valor { get; set; }
        public List<ItemPedidoACLDTO> ItensPedido { get; set; }
    }
}