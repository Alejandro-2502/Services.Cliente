using Cliente.Domain.Entitys;
using Cliente.Domain.Interfaces;
using Cliente.Infrastructure.Context;

namespace Cliente.Infrastructure.Repository;

public class ClienteCommandRepository : IClienteCommandRepository
{
    private readonly DataContext _dataContext;

    public ClienteCommandRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<ClienteEntity> AddAsync(ClienteEntity clienteEntity)
    {
        var result = await _dataContext.Cliente.AddAsync(clienteEntity);
        return result.Entity;
    }

    public async Task<bool> DeleteAsync(ClienteEntity clienteEntity)
    {
        _dataContext.Cliente.Remove(clienteEntity);
        return true;
    }

    public async Task<ClienteEntity> UpdateAsync(ClienteEntity clienteEntity)
    {
        var result =  _dataContext.Cliente.Update(clienteEntity);
        return result.Entity;
    }
}
