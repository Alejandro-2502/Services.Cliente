using Cliente.Aplication.Configurations;
using Cliente.Domain.Entitys;
using Cliente.Infrastructure.Builder;
using Microsoft.EntityFrameworkCore;

namespace Cliente.Infrastructure.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
           : base(options)
    {
    }

    public DbSet<ClienteEntity> Cliente { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlServer(ConfigHelper.ConfigSqlServer!.Connection);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
        .ApplyConfiguration(new ClienteBuilder());

    }
}
