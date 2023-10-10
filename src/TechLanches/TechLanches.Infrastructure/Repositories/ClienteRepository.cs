using Microsoft.EntityFrameworkCore;
using TechLanches.Core;
using TechLanches.Domain.Entities;
using TechLanches.Domain.Repositories;

namespace TechLanches.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly TechLanchesDbContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public ClienteRepository(TechLanchesDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context)); 
    }

    public async Task<Cliente> BuscarPorCpf(string cpf)
    {
        var clientes = await _context.Clientes.ToListAsync();

        return clientes.SingleOrDefault(x => x.CPF.Numero == cpf);
    }

    public async Task<Cliente> Cadastrar(Cliente cliente)
    {
        return (await _context.AddAsync(cliente)).Entity;
    }
}