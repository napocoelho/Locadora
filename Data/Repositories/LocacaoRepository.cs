

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data.Contexts;
using Locadora.Domain.AggregatesModels.LocacaoAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data.Repositories;

public class LocacaoRepository : ILocacaoRepository
{
    private readonly LocacaoContext _context;

    public LocacaoRepository(LocacaoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(_context));
    }

    public void UnitOfWork(Action unitOfWork)
    {
        using IDbContextTransaction dbTransaction = _context.Database.BeginTransaction();

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

    public void Incluir(Locacao item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        _context.Add(item);
        _context.SaveChanges();
    }

    
    public void Excluir(int id)
    {
        Locacao? item = _context.Locacoes.SingleOrDefault(x => x.Id == id);

        if (item is not null)
        {
            _context.Remove(item);
            _context.SaveChanges();

        }
    }

    public void Alterar(Locacao item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        _context.Update(item);
        _context.SaveChanges();
    }

    public Locacao? ObterPorId(int id)
    {
        //var query = 
        //            from locacoes in  _context.Locacoes
        //            join filmes in _context.Filmes
        //                on locacoes.id


        return _context.Locacoes.SingleOrDefault(x => x.Id == id);
    }

    public IEnumerable<Locacao> ObterTodos()
    {
        return _context.Locacoes ?? Enumerable.Empty<Locacao>();
    }


    public bool ClientePossuiLocacao(int idCliente)
    {
        return _context.Locacoes.AsNoTracking().Any(x => x.IdCliente == idCliente && x.DataDevolucao == null);
    }

    public bool FilmePossuiLocacao(int idFilme)
    {
        return _context.Locacoes.AsNoTracking().Any(x => x.IdFilme == idFilme);
    }

    public bool FilmeEstaAlugado(int idFilme)
    {
        return _context.Locacoes.AsNoTracking().Any(x => x.IdFilme == idFilme && x.DataDevolucao == null);
    }

    public Locacao? ObterLocacaoEmAbertoPorIdFilme(int idFilme)
    {
        return _context.Locacoes.AsNoTracking().FirstOrDefault(x => x.IdFilme == idFilme && x.DataDevolucao == null);
    }

}