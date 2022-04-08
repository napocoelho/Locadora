using Locadora.Domain.AggregatesModels.FilmeAggregate;
using Locadora.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Domain.AggregatesModels.LocacaoAggregate;

public class FilmesNovosSpecification : ISpecification<Filme>
{
    public bool IsSatisfiedBy(Filme entity)
    {
        return entity.Lancamento ;
    }
}