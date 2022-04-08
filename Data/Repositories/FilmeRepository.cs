

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data.Contexts;
using Locadora.Domain.AggregatesModels.FilmeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data.Repositories;




public class FilmeRepository : IFilmeRepository
{
    private readonly FilmeContext _filmeContext;


    public FilmeRepository(FilmeContext filmeContext)
    {
        _filmeContext = filmeContext ?? throw new ArgumentNullException(nameof(filmeContext));
    }

    public void UnitOfWork(Action unitOfWork)
    {
        using IDbContextTransaction dbTransaction = _filmeContext.Database.BeginTransaction();

        try
        {
            unitOfWork();

            dbTransaction.Commit();
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            throw ex;
        }
    }

    public void Incluir(Filme item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));


        _filmeContext.Filmes.Add(item);
        _filmeContext.SaveChanges();

    }

    public void Alterar(Filme item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));


        _filmeContext.Filmes.Update(item);
        _filmeContext.SaveChanges();

    }

    public void Excluir(int id)
    {
        Filme? item = _filmeContext.Filmes.FirstOrDefault(x => x.Id == id);

        if (item is not null)
        {
            _filmeContext.Filmes.Remove(item);
            _filmeContext.SaveChanges();
        }
    }

    public Filme? ObterPorId(int id)
    {
        Filme? item = _filmeContext.Filmes.AsNoTracking().SingleOrDefault(x => x.Id == id);
        return item;
    }

    public Filme? ObterPorTitulo(string titulo)
    {
        Filme? item = _filmeContext.Filmes.AsNoTracking().SingleOrDefault(x => x.Titulo == titulo);
        return item;
    }

    public IEnumerable<Filme> ObterTodos()
    {
        return _filmeContext.Filmes ?? Enumerable.Empty<Filme>();
    }

    public IEnumerable<Filme> ObterNaoAlugados()
    {
        IEnumerable<Filme> retorno = from filme in _filmeContext.Filmes
                                     where !_filmeContext.Locacoes.Any(x => x.IdFilme == filme.Id && x.DataDevolucao == null)
                                     select filme;
        //join locacao in _filmeContext.Locacoes on filme.Id equals  locacao.IdFilme

        return retorno;

        //IEnumerable<int> idFilmesAlugados = _filmeContext.Locacoes
        //                                        .Where(x => x.DataDevolucao == null)
        //                                        .Select(x => x.IdFilme);

        //return _filmeContext.Filmes.Where(x => !idFilmesAlugados.Any(w => w != x.Id));
    }
}