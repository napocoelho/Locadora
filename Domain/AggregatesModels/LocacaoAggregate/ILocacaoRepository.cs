

using Locadora.Domain.Interfaces;
namespace Locadora.Domain.AggregatesModels.LocacaoAggregate;

public interface ILocacaoRepository : IRepository<Locacao>
{
    bool ClientePossuiLocacao(int idCliente);
    bool FilmePossuiLocacao(int idFilme);
    bool FilmeEstaAlugado(int idFilme);
    Locacao? ObterLocacaoEmAbertoPorIdFilme(int idFilme);
}