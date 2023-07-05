using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoEFCore.Data
{
    public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.ToTable("PedidoItens");
            builder.HasKey(builder => builder.Id);
            builder.Property(builder => builder.Quantidade).HasDefaultValue(1).IsRequired();
            builder.Property(builder => builder.Valor).IsRequired();
            builder.Property(builder => builder.Desconto).IsRequired();
        }
    }
}