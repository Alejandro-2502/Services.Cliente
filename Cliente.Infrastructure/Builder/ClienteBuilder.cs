using Cliente.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cliente.Infrastructure.Builder;

public class ClienteBuilder : IEntityTypeConfiguration<ClienteEntity>
{
    public void Configure(EntityTypeBuilder<ClienteEntity> entity)
    {
        entity.ToTable("CLIENTE");

        entity.Property(e => e.Nombre)
           .HasMaxLength(30);

        entity.Property(e => e.Apellido)
            .HasMaxLength(30);

        entity.Property(e => e.EMail)
          .HasMaxLength(40);

        entity.Property(e => e.Edad);
        entity.Property(e => e.FechaNacimiento);
        entity.Property(e => e.FechaAlta);
    }
}
