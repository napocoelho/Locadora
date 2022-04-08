using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Locadora.Domain.AggregatesModels.ClienteAggregate;
using Locadora.Domain.AggregatesModels.LocacaoAggregate;

namespace Locadora.Domain.AggregatesModels.ClienteAggregate;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly ILocacaoRepository _locacaoRepository;

    public ClienteService(IClienteRepository clienteRepository, ILocacaoRepository locacaoRepository)
    {
        _clienteRepository = clienteRepository;
        _locacaoRepository = locacaoRepository;
    }

    public void Inserir(Cliente item)
    {
        if (_clienteRepository.ObterPorNome(item.Nome) is not null)
        {
            throw new Exception("Já existe um cliente com o mesmo nome");
        }

        if (_clienteRepository.ObterPorCpf(item.Cpf) is not null)
        {
            throw new Exception("Já existe um cliente com o mesmo CPF");
        }

        _clienteRepository.Incluir(item);
    }

    public void Alterar(Cliente item)
    {
        Cliente clienteDeTeste = _clienteRepository.ObterPorNome(item.Nome);

        if (clienteDeTeste is not null && clienteDeTeste.Id != item.Id)
        {
            throw new Exception("Já existe um cliente com o mesmo nome");
        }

        clienteDeTeste = _clienteRepository.ObterPorCpf(item.Cpf);

        if (clienteDeTeste is not null && clienteDeTeste.Id != item.Id)            
        {
            throw new Exception("Já existe um cliente com o mesmo CPF");
        }

        _clienteRepository.Alterar(item);
    }

    public void Excluir(int idCliente)
    {
        if (!_locacaoRepository.ClientePossuiLocacao(idCliente))
        {
            _clienteRepository.Excluir(idCliente);
        }
        else
        {
            throw new Exception("O cliente não pode ser excluído, pois possui locação");
        }
    }


    public IEnumerable<Cliente> ObterTodos()
    {
        return _clienteRepository.ObterTodos();
    }

    public Cliente? ObterPorId(int id)
    {
        return _clienteRepository.ObterPorId(id);
    }

    public Cliente? ObterPorCpf(string cpf)
    {
        return _clienteRepository.ObterPorCpf(cpf);
    }
}