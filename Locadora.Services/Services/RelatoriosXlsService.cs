using Locadora.Data.Contexts;
using Locadora.Services.Relatorios;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Services.Services;

public class RelatoriosXlsService : IRelatoriosXlsService
{
    readonly IConfiguration _configuration;
    

    public RelatoriosXlsService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IRelatorioXls ClientesEmAtrasoNaDevolucao()
    {
        return new ClientesEmAtrasoNaDevolucaoRelatorioXls(_configuration);
    }

    public IRelatorioXls CincoFilmesMaisAlugados()
    {
        return new CincoFilmesMaisAlugadosRelatorioXls(_configuration);
    }

    public IRelatorioXls SegundoClienteQueMaisAlugou()
    {
        return new SegundoClienteQueMaisAlugouRelatorioXls(_configuration);
    }

    public IRelatorioXls TresFilmesMenosAlugados()
    {
        return new TresFilmesMenosAlugadosRelatorioXls(_configuration);
    }

    public IRelatorioXls FilmesNuncaAlugados()
    {
        return new FilmesNuncaAlugadosRelatorioXls(_configuration);
    }
}
