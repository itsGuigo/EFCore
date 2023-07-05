using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoEFCore.Data
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");
            builder.HasKey(builder => builder.Id);
            builder.Property(builder => builder.IniciadoEm).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(builder => builder.Status).HasConversion<string>();
            builder.Property(builder => builder.TipoFrete).HasConversion<int>();
            builder.Property(builder => builder.Observacao).HasColumnType("VARCHAR(512)");
            
            builder.HasMany(builder => builder.Itens).WithOne(builder => builder.Pedido).OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}