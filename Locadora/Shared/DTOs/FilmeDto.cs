﻿

namespace Locadora.Shared.DTOs;

public class FilmeDto
{
    public int? Id { get; set; }

    public string Titulo { get; set; }
    public int ClassificacaoIndicativa { get; set; }
    public bool Lancamento { get; set; }
}
