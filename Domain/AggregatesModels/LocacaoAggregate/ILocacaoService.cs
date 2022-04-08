using Locadora.Domain.AggregatesModels.FilmeAggregate;
using Locadora.Domain.AggregatesModels.LocacaoAggregate;

namespace Locadora.Domain.AggregatesModels.LocacaoAggregate;

public interface ILocacaoService
{
    void Alterar(Locacao item);
    void Excluir(int idLocacao);
    void Incluir(Locacao item);
    Locacao? ObterPorId(int id);
    IEnumerable<Locacao> ObterTodos();
    void DevolverFilme(int idFilme);
    bool FilmeEstaAlugado(int idFilme);
    Locacao? ObterLocacaoEmAbertoPorIdFilme(int idFilme);

    
}
