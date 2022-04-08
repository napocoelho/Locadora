using Locadora.Domain.AggregatesModels.ClienteAggregate;
using Locadora.Shared.DTOs;

namespace Locadora.Server.Mappers;

public static class ClienteMapper
{
    public static ClienteDto? ToDto(this Cliente? item)
    {
        if (item is null)
            return null;

        return new ClienteDto { Id = item.Id, CPF = item.Cpf, DataNascimento = item.DataNascimento, Nome = item.Nome };
    }

    public static IEnumerable<ClienteDto> ToDto(this IEnumerable<Cliente>? itens)
    {
        return itens?.Where(x => x is not null)
                     .Select(x => x.ToDto())
                         ?? Enumerable.Empty<ClienteDto>();
    }

    public static Cliente? FromDto(this ClienteDto? item)
    {
        if (item is null)
            return null;

        return new Cliente(item.Id, item.Nome, item.CPF, item.DataNascimento);
    }
}
