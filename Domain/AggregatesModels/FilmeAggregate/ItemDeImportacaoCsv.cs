using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Domain.AggregatesModels.FilmeAggregate;

public class ItemDeImportacaoCsv
{
    public Filme? Filme { get; set; }
    public string? Erro { get; set; }
}
