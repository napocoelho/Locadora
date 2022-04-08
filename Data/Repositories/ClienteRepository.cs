
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Locadora.Domain.AggregatesModels.ClienteAggregate;

using MySql;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
//using Dapper;


namespace Data.Repositories;




public class ClienteRepository : IClienteRepository
{
    private readonly ClienteContext _context;



    public ClienteRepository(ClienteContext context)
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

    public void Incluir(Cliente item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        _context.Clientes.Add(item);
        _context.SaveChanges();
    }

    public void Alterar(Cliente item)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        _context.Clientes.Update(item);
        _context.SaveChanges();
    }

    public void Excluir(int id)
    {

        Cliente? item = _context.Clientes.FirstOrDefault(x => x.Id == id);

        if (item is not null)
        {
            _context.Clientes.Remove(item);
            _context.SaveChanges();
        }

    }

    public Cliente? ObterPorId(int id)
    {
        Cliente? item = _context.Clientes.AsNoTracking().SingleOrDefault(x => x.Id == id);
        return item;
    }

    public IEnumerable<Cliente> ObterTodos()
    {
        return _context.Clientes.AsNoTracking() ?? Enumerable.Empty<Cliente>();

    }





    public Cliente? ObterPorNome(string nome)
    {
        return _context?.Clientes?.AsNoTracking().FirstOrDefault(x => x.Nome == nome);
    }

    public Cliente? ObterPorCpf(string cpf)
    {
        return _context?.Clientes?.AsNoTracking().FirstOrDefault(x => x.Cpf == cpf);
    }
}