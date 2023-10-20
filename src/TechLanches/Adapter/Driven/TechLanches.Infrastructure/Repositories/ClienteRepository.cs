using Microsoft.EntityFrameworkCore;
using TechLanches.Core;
using TechLanches.Domain.Entities;
using TechLanches.Domain.Ports.Repositories;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly TechLanchesDbContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public ClienteRepository(TechLanchesDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context)); 
    }

    public async Task<Cliente> BuscarPorCpf(Cpf cpf)
    {
        return await _context.Clientes.SingleOrDefaultAsync(x => x.CPF.Numero == cpf.Numero);
    }

    public async Task<Cliente> Cadastrar(Cliente cliente)
    {
        return (await _context.AddAsync(cliente)).Entity;
    }
}