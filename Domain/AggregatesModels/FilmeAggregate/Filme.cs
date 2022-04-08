using Locadora.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Domain.AggregatesModels.FilmeAggregate;

[Table("Filme")]
public class Filme : IEntity, IAggregateRoot
{
    public int? Id { get; set; }

    public string Titulo { get; set; }
    public int ClassificacaoIndicativa { get; set; }
    public bool Lancamento { get; set; }

    public Filme(int? id, string titulo, int classificacaoIndicativa, bool lancamento)
    {
        ArgumentNullException.ThrowIfNull(titulo, nameof(titulo));
        ArgumentNullException.ThrowIfNull(classificacaoIndicativa, nameof(classificacaoIndicativa));
        ArgumentNullException.ThrowIfNull(lancamento, nameof(lancamento));

        Id = id;
        Titulo = titulo;
        ClassificacaoIndicativa = classificacaoIndicativa;
        Lancamento = lancamento;
    }
}