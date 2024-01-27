using TechLanches.Core;
using TechLanches.Domain.Enums;

namespace TechLanches.Domain.Aggregates;

public class Pagamento : Entity, IAggregateRoot
{
    private Pagamento() { }

    public Pagamento(int pedidoId, decimal valor, FormaPagamento formaPagamento)
    {
        PedidoId = pedidoId;
        Valor = valor;
        StatusPagamento = StatusPagamento.Aguardando;
        FormaPagamento = formaPagamento;
        Validar();
    }

    public Pagamento(int pagamentoId, int pedidoId, decimal valor, StatusPagamento statusPagamento) : base(pagamentoId)
    {
        PedidoId = pedidoId;
        Valor = valor;
        StatusPagamento = statusPagamento;
        Validar();
    }

    public int PedidoId { get; private set; }
    public decimal Valor { get; private set; }
    public StatusPagamento StatusPagamento { get; private set; }
    public FormaPagamento FormaPagamento { get; private set; }
    public Pedido Pedido { get; private set; }

    public void Validar()
    {
        if (PedidoId <= 0)
            throw new DomainException("O pagamento deve possuir um pedido.");

        if (Valor <= 0)
            throw new DomainException("O valor deve ser maior que zero.");

        if (StatusPagamento != StatusPagamento.Aguardando && Id <= 0)
            throw new DomainException("O pagamento deve iniciar como aguardando.");
    }

    public void Reprovar()
    {
        if (StatusPagamento == StatusPagamento.Aprovado)
            throw new DomainException("O pagamento já foi aprovado, assim não podendo reprova-lo.");

        StatusPagamento = StatusPagamento.Reprovado;
    }

    public void Aprovar()
    {
        StatusPagamento = StatusPagamento.Aprovado;
    }
}
