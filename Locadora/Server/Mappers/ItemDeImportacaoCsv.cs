using Locadora.Domain.AggregatesModels.FilmeAggregate;
using Locadora.Shared.DTOs;

namespace Locadora.Server.Mappers;

public static class ItemDeImportacaoCsvMapper
{

    public static ItemDeImportacaoCsvDto? ToDto(this ItemDeImportacaoCsv? item)
    {
        if (item is null)
            return null;

        return new ItemDeImportacaoCsvDto { Erro = item.Erro, Filme = item.Filme.ToDto() };
    }

    public static IEnumerable<ItemDeImportacaoCsvDto> ToDto(this IEnumerable<ItemDeImportacaoCsv>? itens)
    {
        return itens?.Where(x => x is not null)?.Select(x => x.ToDto()) ?? Enumerable.Empty<ItemDeImportacaoCsvDto>();
    }

    public static ItemDeImportacaoCsv? FromDto(this ItemDeImportacaoCsvDto? item)
    {
        if (item is null)
            return null;

        return new ItemDeImportacaoCsv() { Erro = item.Erro, Filme = item.Filme.FromDto() };
    }
}
