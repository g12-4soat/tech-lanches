using TechLanches.Core;
using TechLanches.Domain.Enums;
using TechLanches.Domain.Validations;

namespace TechLanches.Domain.Services
{
    public class StatusPedidoValidacaoService : IStatusPedidoValidacaoService
    {
        private readonly IEnumerable<IStatusPedidoValidacao> Validacoes;

        public StatusPedidoValidacaoService(IEnumerable<IStatusPedidoValidacao> validacoes)
        {
            Validacoes = validacoes;
        }

        public void Validar(StatusPedido statusPedido, StatusPedido novoStatusPedido)
        {
            var validacao = Validacoes.FirstOrDefault(x => x.StatusPedido == novoStatusPedido)
                ?? throw new NotImplementedException($"{nameof(StatusPedido)} {novoStatusPedido} não implementado.");

            var valido = validacao.Validar(statusPedido);

            if (!valido) throw new DomainException("O status selecionado não é válido");
        }
    }
}
