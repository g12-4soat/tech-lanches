namespace TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs
{
    public record ItemPedidoACLDTO
    {
        public int Quantidade { get; set; }
        public decimal PrecoProduto { get; set; }
        public string NomeProduto { get; set; }
    }
}