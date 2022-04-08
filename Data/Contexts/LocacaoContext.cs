
using Microsoft.EntityFrameworkCore;
using Locadora.Domain.AggregatesModels.LocacaoAggregate;
using Locadora.Domain.AggregatesModels.FilmeAggregate;
using Locadora.Domain.AggregatesModels.ClienteAggregate;

namespace Data.Contexts;

public class LocacaoContext : DbContext
{
    public DbSet<Locacao> Locacoes { get; set; }
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    

    public LocacaoContext(DbContextOptions<LocacaoContext> options) : base(options)
    {

    }
}