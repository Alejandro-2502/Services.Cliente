using Cliente.Domain.Entitys;
using Cliente.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Cliente.Infrastructure.Repository;
public class ClienteQuerysRepository : IClienteQuerysRepository
{
    private readonly DataContext _dataContext;

    public ClienteQuerysRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<ClienteEntity>> GetAllAsync()
    {
        try
        {
            return await _dataContext.Cliente.ToListAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<ClienteEntity> GetAsync(int id)
    {
        try
        {
            var result = await _dataContext.Cliente
                        .Where(cli => cli.Id == id).FirstOrDefaultAsync();

            return result;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<List<ClienteEntity>> GetByAgeMoreThan(int age)
    {
        try
        {
            var result = await _dataContext.Cliente
                .Where(cli => cli.Edad >= age)
                .OrderByDescending(cli => cli.Id)
                .ToListAsync();
            return result;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<List<ClienteEntity>> GetByNameLike(string name)
    {
        try
        {
            var result = await _dataContext.Cliente
                .Where(cli => EF.Functions.Like(cli.Nombre, name))
                .ToListAsync();

            return result;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
