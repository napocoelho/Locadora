using Locadora.Shared.DTOs;
using Locadora.Domain.AggregatesModels.LocacaoAggregate;

namespace Locadora.Server.Mappers;

public static class LocacaoMapper
{
    public static LocacaoDto? ToDto(this Locacao? item)
    {
        if (item is null)
            return null;

        return new LocacaoDto { Id = item.Id, DataDevolucao = item.DataDevolucao, DataLocacao = item.DataLocacao, IdCliente = item.IdCliente, IdFilme = item.IdFilme };
    }

    public static IEnumerable<LocacaoDto> ToDto(this IEnumerable<Locacao>? itens)
    {
        return itens?.Select(x => x.ToDto()) ?? Enumerable.Empty<LocacaoDto>();
    }

    public static Locacao? FromDto(this LocacaoDto? item)
    {
        if (item is null)
            return null;

        return new Locacao(item.Id, item.DataLocacao, item.DataDevolucao, item.IdCliente, item.IdFilme);
    }
}