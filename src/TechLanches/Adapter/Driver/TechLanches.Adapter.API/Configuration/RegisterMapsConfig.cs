using Mapster;
using System.Reflection;
using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Adapter.API.Constantes;
using TechLanches.Application.DTOs;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Adapter.API.Configuration
{
    public static class RegisterMapsConfig
    {
#pragma warning disable IDE0060 // Remove unused parameter
        public static void RegisterMaps(this IServiceCollection services)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            TypeAdapterConfig<Produto, ProdutoResponseDTO>.NewConfig()
                .Map(dest => dest.Categoria, src => CategoriaProduto.From(src.Categoria.Id));

#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            TypeAdapterConfig<Pedido, PedidoResponseDTO>.NewConfig()
                .Map(dest => dest.NomeStatusPedido, src => src.StatusPedido.ToString())
                .Map(dest => dest.NomeCliente, src => src.Cliente == null ? MensagensConstantes.CLIENTE_NAO_IDENTIFICADO : src.Cliente.Nome);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8604 // Possible null reference argument.

            TypeAdapterConfig<Pedido, PedidoACLDTO>.NewConfig();

            TypeAdapterConfig<ItemPedido, ItemPedidoACLDTO>.NewConfig()
               .Map(dest => dest.NomeProduto, src => src.Produto.Nome);

            TypeAdapterConfig<ItemPedido, ItemPedidoResponseDTO>.NewConfig()
                .Map(dest => dest.Produto.Nome, src => src.Produto.Nome);

            TypeAdapterConfig<Cliente, ClienteResponseDTO>.NewConfig()
                .Map(dest => dest.Email, src => src.Email.EnderecoEmail)
                .Map(dest => dest.CPF, src => src.CPF.Numero);

            TypeAdapterConfig<Pagamento, PagamentoResponseDTO>.NewConfig()
                .Map(dest => dest.Id, src => src.PedidoId)
                .Map(dest => dest.StatusPagamento, src => src.StatusPagamento.ToString());

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}