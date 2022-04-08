
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Locadora.Domain.AggregatesModels.FilmeAggregate;
using Locadora.Domain.AggregatesModels.ClienteAggregate;
using Locadora.Domain.AggregatesModels.LocacaoAggregate;
using Locadora.Domain.Interfaces;



namespace Locadora.Domain.AggregatesModels.LocacaoAggregate;


public class LocacaoService : ILocacaoService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IFilmeRepository _filmeRepository;
    private readonly ILocacaoRepository _locacaoRepository;

    public LocacaoService(ILocacaoRepository locacaoRepository, IClienteRepository clienteRepository, IFilmeRepository filmeRepository)
    {
        _locacaoRepository = locacaoRepository;
        _clienteRepository = clienteRepository;
        _filmeRepository = filmeRepository;
    }

    public void Incluir(Locacao item)
    {



        Cliente? cliente = _clienteRepository.ObterPorId(item.IdCliente);

        if (cliente is null)
        {
            throw new Exception($"O cliente especificado ({item.IdCliente}) não existe");
        }

        Filme? filme = _filmeRepository.ObterPorId(item.IdFilme);

        if (filme is null)
        {
            throw new Exception($"O filme especificado ({item.IdFilme}) não existe");
        }

        if (_locacaoRepository.FilmeEstaAlugado(item.IdFilme))
        {
            throw new Exception($"O filme '{filme.Titulo}' já se encontra alugado");
        }

        //ISpecification<Filme> especificacaoFilmeNovo = new FilmesNovosSpecification();
        //int diasDeLocacao = especificacaoFilmeNovo.IsSatisfiedBy(filme) ? 2 : 3;
        //item.DataDevolucao = item.DataLocacao.AddDays(diasDeLocacao);

        _locacaoRepository.Incluir(item);
    }

    public void Alterar(Locacao item)
    {

        _locacaoRepository.Alterar(item);
    }

    public void Excluir(int idLocacao)
    {
        _locacaoRepository.Excluir(idLocacao);
    }


    public IEnumerable<Locacao> ObterTodos()
    {
        return _locacaoRepository.ObterTodos();
    }

    public Locacao? ObterPorId(int id)
    {
        return _locacaoRepository.ObterPorId(id);
    }



    public void DevolverFilme(int idFilme)
    {
        Locacao? aluguel = _locacaoRepository.ObterLocacaoEmAbertoPorIdFilme(idFilme);

        if (aluguel is not null)
        {
            aluguel.DataDevolucao = DateTime.Today;
            Alterar(aluguel);
        }
        else
        {
            throw new Exception("O filme não está alugado");
        }


    }

    public bool FilmeEstaAlugado(int idFilme)
    {
        return _locacaoRepository.FilmeEstaAlugado(idFilme);
    }


    public Locacao? ObterLocacaoEmAbertoPorIdFilme(int idFilme)
    {
        return _locacaoRepository.ObterLocacaoEmAbertoPorIdFilme(idFilme);
    }
}