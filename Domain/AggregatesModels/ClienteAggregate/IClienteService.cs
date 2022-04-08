using Locadora.Domain.AggregatesModels.ClienteAggregate;

namespace Locadora.Domain.AggregatesModels.ClienteAggregate;

public interface IClienteService
{
    void Alterar(Cliente item);
    void Excluir(int idCliente);
    void Inserir(Cliente item);


    IEnumerable<Cliente> ObterTodos();
    Cliente? ObterPorId(int id);
    Cliente? ObterPorCpf(string cpf);
}
