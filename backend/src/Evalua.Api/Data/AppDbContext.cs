using Evalua.Api.Data.Sp;
using Evalua.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Evalua.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Pais> Paises => Set<Pais>();

    public DbSet<Cliente> Clientes => Set<Cliente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pais>(entity =>
        {
            entity.ToTable("Paises");
            entity.HasKey(e => e.IdPais);
            entity.Property(e => e.Nombre).HasMaxLength(120).IsRequired();
            entity.HasIndex(e => e.Nombre).IsUnique();
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Clientes");
            entity.HasKey(e => e.IdCliente);
            entity.Property(e => e.NombreCompleto).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Telefono).HasMaxLength(32).IsRequired();
            entity.HasOne(e => e.Pais)
                .WithMany(p => p.Clientes)
                .HasForeignKey(e => e.IdPais)
                .HasConstraintName("FK_Clientes_Paises_IdPais");
        });

        modelBuilder.Entity<ClientePaginadoSpRow>(entity =>
        {
            entity.HasNoKey();
        });
    }
}
