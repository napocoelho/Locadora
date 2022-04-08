

using Locadora.Domain.Interfaces;

namespace Locadora.Domain.AggregatesModels.ClienteAggregate;

public interface IClienteRepository : IRepository<Cliente>
{
    Cliente? ObterPorNome(string nome);
    Cliente? ObterPorCpf(string cpf);
}