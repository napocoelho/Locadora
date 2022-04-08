using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Domain.Interfaces;

public interface IRepository<T> where T : IAggregateRoot
{
    void UnitOfWork(Action unitOfWork);
    IEnumerable<T> ObterTodos();
    T? ObterPorId(int id);

    void Incluir(T item);
    void Alterar(T item);
    void Excluir(int id);
}