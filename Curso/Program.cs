using System;
using System.Linq;
using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        // static void Main(string[] args)
        // {
        //     using var db = new Data.ApplicationContext();
        //     db.Database.Migrate();//usado só pra desenvolvimento, pra produção não é bom

        //var existe = db.Database.GetPendingMigrations().Any();
        // if(existe){
        //     Console.WriteLine("Tem migrações pendentes");
        // }
        // }
        static void Main(string[] args)
        {
            #region selects
            //ConsultarPedidoCarregamentoAdiantado();
            //ConsultarDados();
            #endregion
            #region updates
            //AtualizarDados();
            #endregion
            #region inserts
            //CadastrarPedido();
            //InserirDados();
            //InserirDadosEmMassa();
            #endregion
            #region delete
            RemoverRegistro();
            #endregion
        }

        private static void RemoverRegistro(){
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes?.Find(2);
            db.Clientes?.Remove(cliente);
            // db.Remove(cliente);
            // db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
        }

        private static void AtualizarDados()
        {
            using var db = new Data.ApplicationContext();
            // var cliente = db.Clientes?.FirstOrDefault(p => p.Id == 1);
            //var cliente = db.Clientes?.Find(1);

            var cliente = new Cliente{
                Id = 1
            };

            //cliente.Nome = "Cliente Alterado Passo2";

            var clienteDesconectado = new {
                Nome = "Cliente Desconectado Passo 3",
                Telefone = "40028922"
            };

            db.Attach(cliente);
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);
            //db.Entry(cliente).State = EntityState.Modified;

            //db.Clientes?.Update(cliente);
            db.SaveChanges();
            db.Dispose();
        }

        private static void ConsultarPedidoCarregamentoAdiantado(){
            try
            {
                using var db = new Data.ApplicationContext();
                var pedidos = db.Pedidos?
                .Include(gg => gg.Itens)
                .ThenInclude(gg => gg.Produto)
                .ToList();

                Console.WriteLine(pedidos?.Count);
            }
            catch(Exception ex){
                throw;
            }
               
        }
        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();
            //var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
            var consultaPorMetodo = db.Clientes?.Where(gg => gg.Id > 0).ToList();

            if (consultaPorMetodo != null && consultaPorMetodo.Any())
            {
                foreach (var cliente in consultaPorMetodo)
                {
                    Console.WriteLine($"Consultando Cliente: {cliente.Id}");
                    //Console.WriteLine(String.Format("Consultando Cliente: {0}", cliente.Id));

                    //db.Clientes?.Find(cliente.Id);
                    db.Clientes?.FirstOrDefault(gg => gg.Id == cliente.Id);
                }
            }

        }

        #region Inserções

        private static void CadastrarPedido()
        {
            try
            {
                using var db = new Data.ApplicationContext();

                var cliente = db.Clientes?.FirstOrDefault();
                var produto = db.Produtos?.FirstOrDefault(); // o erro ta aqui
                if (cliente != null && produto != null)
                {
                    var pedido = new Pedido
                    {
                        ClienteId = cliente.Id,
                        IniciadoEm = DateTime.Now,
                        FinalizadoEm = DateTime.Now,
                        Observacao = "Pedido Teste",
                        Status = StatusPedido.Analise,
                        TipoFrete = TipoFrete.SemFrete,
                        Itens = new List<PedidoItem>
                        {
                            new PedidoItem
                            {
                                ProdutoId = produto.Id,
                                Desconto = 0,
                                Quantidade = 1,
                                Valor = 10,
                            }
                        }
                    };
                    db.Pedidos?.Add(pedido);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }

        }
        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "40028922",
                Valor = "10m",
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };
            var cliente = new Cliente
            {
                Nome = "Produto Teste",
                CEP = "08098360",
                Cidade = "Guarulhos",
                Estado = "SP",
                Telefone = "11940028922"
            };

            var listaClientes = new[]
            {
                new Cliente{
                    Nome = "Teste1",
                    CEP = "08098360",
                    Cidade = "Guarulhos",
                    Estado = "SP",
                    Telefone = "11940028922"
                },
                new Cliente{
                    Nome = "Teste2",
                    CEP = "12312312",
                    Cidade = "Guarulhos",
                    Estado = "SP",
                    Telefone = "11940028922"
                },
            };

            using var db = new Data.ApplicationContext();
            //db.AddRange(produto, cliente);
            db.Set<Cliente>().AddRange(listaClientes);//outra maneira de adicionar tambem usando array
            //db.Clientes.AddRange(listaClientes); //exemplo adicionando array

            var registros = db.SaveChanges();
            //Console.WriteLine($"Total de registros adicionados em massa: {registros}");
            Console.WriteLine(String.Format("Total de registros adicionados em massa: {0}", registros));
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "40028922",
                Valor = "10m",
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };
            using var db = new Data.ApplicationContext();
            //formas de adicionar no banco de dados
            //db.Produtos.Add(produto);
            db.Set<Produto>().Add(produto);
            //db.Entry(produto).State = EntityState.Added;
            //db.Add(produto);

            //só salva se chamar o savechanges()
            var registros = db.SaveChanges();
            Console.WriteLine($"Total registro(s): {registros}");
            Console.WriteLine(String.Format("Total registro(s): {0}", registros));
        }
        #endregion
    }
}