using Infinite.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infinite.Core.Context
{
    public class InfiniteContext : DbContext
    {
        public InfiniteContext(DbContextOptions<InfiniteContext> opt) : base(opt)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemCarrinhoEntity>()
                .HasKey(e => new {e.CarrinhoID, e.ProdutoID});

            modelBuilder.Entity<ItemCarrinhoEntity>()
                .HasOne<CarrinhoEntity>()
                .WithMany(g => g.Produtos)
                .HasForeignKey(s => s.CarrinhoID);

            modelBuilder.Entity<CartaoClienteEntity>()
                .HasKey(e => new { e.ClienteId, e.CartaoId });

            modelBuilder.Entity<CartaoClienteEntity>()
                .HasOne<ClienteEntity>()
                .WithMany(g => g.Cartoes)
                .HasForeignKey(s => s.ClienteId);

            modelBuilder.Entity<EnderecoClienteEntity>()
                .HasKey(e => new { e.ClienteId, e.EnderecoId });

            modelBuilder.Entity<EnderecoClienteEntity>()
                .HasOne<ClienteEntity>()
                .WithMany(g => g.Enderecos)
                .HasForeignKey(s => s.ClienteId);

            modelBuilder.Entity<FotoProdutoEntity>()
                .HasKey(e => new { e.ProdutoId, e.ArquivoId });

            modelBuilder.Entity<FotoProdutoEntity>()
                .HasOne<ProdutoEntity>()
                .WithMany(g => g.Fotos)
                .HasForeignKey(s => s.ProdutoId);
        }

        //DataSets
        public DbSet<FuncionarioEntity> Funcionario { get; set; }
        public DbSet<CupomEntity> Cupom { get; set; }
        public DbSet<FormaPagEntity> FormaPag { get; set; }
        public DbSet<PagamentoEntity> Pagamento { get; set; }
        public DbSet<CompraEntity> Compra { get; set; }
        public DbSet<ClienteEntity> Cliente { get; set; }
        public DbSet<CartaoEntity> Cartao { get; set; }
        public DbSet<CartaoClienteEntity> CartaoCliente { get; set; }
        public DbSet<EnderecoEntity> Endereco { get; set; }
        public DbSet<EnderecoClienteEntity> EnderecoCliente { get; set; }
        public DbSet<JogoEntity> Jogo { get; set; }
        public DbSet<MaquinaEntity> Maquina { get; set; }
        public DbSet<AgendamentoEntity> Agendamento { get; set; }
        public DbSet<CategoriaEntity> Categoria { get; set; }
        public DbSet<ProdutoEntity> Produto{ get; set; }
        public DbSet<ItemCarrinhoEntity> ItemCarrinho { get; set; }
        public DbSet<CarrinhoEntity> Carrinho { get; set; }
        public DbSet<UsuarioEntity> Usuario { get; set; }
        public DbSet<TipoUsuarioEntity> TipoUsuario { get; set; }
        public DbSet<TipoCupomEntity> TipoCupom { get; set; }
        public DbSet<ArquivoEntity> Arquivo { get; set; }
        public DbSet<FotoProdutoEntity> FotoProduto { get; set; }
        public DbSet<VisitantesEntity> Visitantes { get; set; }
    }
}
