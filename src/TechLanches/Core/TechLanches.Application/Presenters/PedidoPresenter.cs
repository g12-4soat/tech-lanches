using Mapster;
using TechLanches.Application.DTOs;
using TechLanches.Application.Presenters.Interfaces;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.Presenters
{
    public class PedidoPresenter : IPedidoPresenter
    {
        public Adapter.ACL.Pagamento.QrCode.DTOs.PedidoACLDTO ParaAclDto(PedidoResponseDTO pedidoDto)
        {
            var pedido = new Adapter.ACL.Pagamento.QrCode.DTOs.PedidoACLDTO()
            {
                ReferenciaExterna = pedidoDto.Id.ToString(),
                TotalTransacao = pedidoDto.Valor,
                ItensPedido = new List<Adapter.ACL.Pagamento.QrCode.DTOs.ItemPedidoACLDTO >(),
                Titulo = "Compra em TechLanches",
                Descricao = "Compra e Retirada de produto",
                DataExpiracao = DateTime.Now.AddHours(1).ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"),
                UrlNotificacao = "https://spider-tight-previously.ngrok-free.app/api/pagamentos/webhook/mercadopago" //alterar para endpoint de pagamento para receber notificacao
            };

            foreach (var item in pedidoDto.ItensPedido)
            {
                var itemPedido = new Adapter.ACL.Pagamento.QrCode.DTOs.ItemPedidoACLDTO()
                {
                    Categoria = CategoriaProduto.From(item.Produto.CategoriaId).Nome,
                    NomeProduto = item.Produto.Nome,
                    Descricao = item.Produto.Descricao,
                    Quantidade = item.Quantidade,
                    UnidadeMedida = "unit",
                    PrecoProduto = item.Produto.Preco,
                    TotalTransacao = item.Valor
                };

                pedido.ItensPedido.Add(itemPedido);
            }
            return pedido;
        }

        public PedidoResponseDTO ParaDto(Pedido entidade)
        {
            return entidade.Adapt<PedidoResponseDTO>();
        }

        public List<PedidoResponseDTO> ParaListaDto(List<Pedido> entidade)
        {
            return entidade.Adapt<List<PedidoResponseDTO>>();
        }
    }
}
