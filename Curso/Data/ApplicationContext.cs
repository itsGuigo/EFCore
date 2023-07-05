using System.Linq;
using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CursoEFCore.Data{
    public class ApplicationContext : DbContext
    {
        //private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());

        public DbSet<Pedido>? Pedidos{get; set;}
        public DbSet<Produto>? Produtos{get; set;}
        public DbSet<Cliente>? Clientes{get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                // .UseLoggerFactory(_logger)
                // .EnableSensitiveDataLogging()
                .UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalog=CursoEFCore;Integrated Security=true",
                p => p.EnableRetryOnFailure(maxRetryCount: 2, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null)
                                            .MigrationsHistoryTable("curso_ef_core"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
            MapearPropriedadesEsquecidas(modelBuilder);

            // modelBuilder.Entity<Pedido>();

            // modelBuilder.Entity<Cliente>(gg =>{
            //     gg.ToTable("Clientes");

            //     gg.HasKey(gg => gg.Id);
            //     gg.Property(gg => gg.Nome).HasColumnType("NVARCHAR(80)").IsRequired();
            //     gg.Property(gg => gg.Telefone).HasColumnType("CHAR(11)");
            //     gg.Property(gg => gg.CEP).HasColumnType("CHAR(8)").IsRequired();
            //     gg.Property(gg => gg.Estado).HasColumnType("CHAR(2)").IsRequired();
            //     gg.Property(gg => gg.Cidade).HasMaxLength(60).IsRequired();

            //     gg.HasIndex(i => i.Telefone).HasName("idx_cliente_telefone");
            // });

            // modelBuilder.Entity<Produto>(gg =>{
            //     gg.ToTable("Produtos");
            //     gg.HasKey(gg => gg.Id);
            //     gg.Property(gg => gg.CodigoBarras).HasColumnType("VARCHAR(14)").IsRequired();
            //     gg.Property(gg => gg.Descricao).HasColumnType("VARCHAR(60)");
            //     gg.Property(gg => gg.Valor).IsRequired();
            //     gg.Property(gg => gg.TipoProduto).HasConversion<string>();

            // });

            // modelBuilder.Entity<Pedido>(gg =>{
            //     gg.ToTable("Pedidos");
            //     gg.HasKey(gg => gg.Id);
            //     gg.Property(gg => gg.IniciadoEm).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            //     gg.Property(gg => gg.Status).HasConversion<string>();
            //     gg.Property(gg => gg.TipoFrete).HasConversion<int>();
            //     gg.Property(gg => gg.Observacao).HasColumnType("VARCHAR(512)");

            //     gg.HasMany(gg => gg.Itens).WithOne(gg => gg.Pedido).OnDelete(DeleteBehavior.Cascade);
            // });

            // modelBuilder.Entity<PedidoItem>(gg =>{
            //     gg.ToTable("PedidoItens");
            //     gg.HasKey(gg => gg.Id);
            //     gg.Property(gg => gg.Quantidade).HasDefaultValue(1).IsRequired();
            //     gg.Property(gg => gg.Valor).IsRequired();
            //     gg.Property(gg => gg.Desconto).IsRequired();
            // });
        }

        //mapeia as propriedades que não foram mapeadas ainda
        protected void MapearPropriedadesEsquecidas(ModelBuilder modelBuilder){
            foreach(var entity in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entity.GetProperties().Where(p => p.ClrType == typeof(string));
                foreach(var property in properties){
                    if(string.IsNullOrEmpty(property.GetColumnType()) && !property.GetMaxLength().HasValue)
                    {
                        //property.SetMaxLength(100);
                        property.SetColumnType("VARCHAR(100)");
                    }
                }
            }
        }
    }
}