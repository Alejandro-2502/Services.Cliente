using Cliente.Infrastructure.Repository;

namespace Cliente.Domain.Interfaces;

public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync();
    IClienteCommandRepository clienteCommandRepository { get; }
    IClienteQuerysRepository clienteQuerysRepository { get; }
}
