namespace TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs
{
    public class PedidoACLDTO
    {
        public decimal Valor { get; set; }
        public List<ItemPedidoACLDTO> ItensPedido { get; set; }
    }
}