

using Microsoft.EntityFrameworkCore;

using Locadora.Domain.AggregatesModels.FilmeAggregate;
using Locadora.Domain.AggregatesModels.LocacaoAggregate;

namespace Data.Contexts;

public class FilmeContext : DbContext
{
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Locacao> Locacoes { get; set; }


    public FilmeContext(DbContextOptions<FilmeContext> options) : base(options)
    {

    }
}