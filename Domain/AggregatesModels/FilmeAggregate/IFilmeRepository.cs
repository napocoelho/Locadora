

using Locadora.Domain.Interfaces;

namespace Locadora.Domain.AggregatesModels.FilmeAggregate;

public interface IFilmeRepository : IRepository<Filme>
{
    Filme? ObterPorTitulo(string titulo);
    IEnumerable<Filme> ObterNaoAlugados();
}
