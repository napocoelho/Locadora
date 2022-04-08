using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Shared.DTOs;

public class ItemDeImportacaoCsvDto
{
    public FilmeDto? Filme { get; set; }
    public string? Erro { get; set; }
}