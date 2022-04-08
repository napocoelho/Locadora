
using Locadora.Shared.DTOs;
using Locadora.Domain.AggregatesModels.FilmeAggregate;

namespace Locadora.Server.Mappers;

public static class FilmeMapper
{
    public static FilmeDto? ToDto(this Filme? item)
    {
        if (item is null)
            return null;

        return new FilmeDto { Id = item.Id, ClassificacaoIndicativa = item.ClassificacaoIndicativa, Lancamento = item.Lancamento, Titulo = item.Titulo };
    }

    public static Filme? FromDto(this FilmeDto? item)
    {
        if (item is null)
            return null;

        return new Filme(item.Id, item.Titulo, item.ClassificacaoIndicativa, item.Lancamento);
    }

    public static IEnumerable<FilmeDto> ToDto(this IEnumerable<Filme>? enumerable)
    {
        return enumerable?.Where(x => x is not null).Select(x => x.ToDto()) ?? Enumerable.Empty<FilmeDto>();
    }
}