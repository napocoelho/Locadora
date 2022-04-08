using Locadora.Domain.AggregatesModels.FilmeAggregate;

namespace Locadora.Domain.AggregatesModels.FilmeAggregate;

public interface IFilmeService
{
    void Alterar(Filme item);
    void Excluir(int idFilme);
    void Incluir(Filme item);
    Filme? ObterPorId(int id);
    Filme? ObterPorTitulo(string titulo);
    IEnumerable<Filme> ObterTodos();
    IEnumerable<Filme> ObterNaoAlugados();

    IEnumerable<ItemDeImportacaoCsv> ImportarFilmes(Filme[] filmes);
}
