using Cliente.Domain.Interfaces;
using Cliente.Infrastructure.Context;
using Cliente.Infrastructure.Repository;

namespace Cliente.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dataContext;
    public IClienteCommandRepository _clienteCommandRepository;
    public IClienteQuerysRepository _clienteQuerysRepository;

    public UnitOfWork(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public IClienteCommandRepository clienteCommandRepository => _clienteCommandRepository =
               _clienteCommandRepository ?? new ClienteCommandRepository(_dataContext);

    public IClienteQuerysRepository clienteQuerysRepository => _clienteQuerysRepository =
        _clienteQuerysRepository ?? new ClienteQuerysRepository(_dataContext);

    public async Task<int> SaveChangesAsync()
    {
        return await _dataContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dataContext.Dispose();
    }
}
