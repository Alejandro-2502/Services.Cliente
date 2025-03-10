using Cliente.Domain.Entitys;

namespace Cliente.Domain.Interfaces;

public interface IClienteCommandRepository
{
    Task<ClienteEntity> AddAsync(ClienteEntity clienteEntity);
    Task <ClienteEntity> UpdateAsync(ClienteEntity clienteEntity);
    Task<bool> DeleteAsync(ClienteEntity clienteEntity);   
}
