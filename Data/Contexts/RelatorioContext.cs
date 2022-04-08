using Microsoft.EntityFrameworkCore;
using Locadora.Domain.AggregatesModels.ClienteAggregate;
using Locadora.Domain.AggregatesModels.LocacaoAggregate;
using Locadora.Domain.AggregatesModels.FilmeAggregate;

namespace Locadora.Data.Contexts;




public class RelatorioContext : DbContext
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Locacao> Locacoes { get; set; }



    public RelatorioContext(DbContextOptions<RelatorioContext> options) : base(options)
    {

    }
}