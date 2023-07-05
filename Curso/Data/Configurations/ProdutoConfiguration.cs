using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoEFCore.Data
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");
            builder.HasKey(builder => builder.Id);
            builder.Property(builder => builder.CodigoBarras).HasColumnType("VARCHAR(14)").IsRequired();
            builder.Property(builder => builder.Descricao).HasColumnType("VARCHAR(60)");
            builder.Property(builder => builder.Valor).IsRequired();
            builder.Property(builder => builder.TipoProduto).HasConversion<string>();
        }
    }
}