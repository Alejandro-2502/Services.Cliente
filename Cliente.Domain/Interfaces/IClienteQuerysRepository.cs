using Cliente.Domain.Entitys;
using Cliente.Domain.Interfaces;

namespace Cliente.Infrastructure.Repository
{
    public interface IClienteQuerysRepository 
    {
        Task<List<ClienteEntity>> GetByAgeMoreThan (int age);
        Task<List<ClienteEntity>> GetByNameLike (string name);
        Task<List<ClienteEntity>> GetAllAsync();
        Task<ClienteEntity> GetAsync(int id);
    }
}
