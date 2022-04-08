using Locadora.Services.Relatorios;

namespace Locadora.Services.Services
{
    public interface IRelatoriosXlsService
    {
        IRelatorioXls CincoFilmesMaisAlugados();
        IRelatorioXls ClientesEmAtrasoNaDevolucao();
        IRelatorioXls SegundoClienteQueMaisAlugou();
        IRelatorioXls TresFilmesMenosAlugados();
        IRelatorioXls FilmesNuncaAlugados();
    }
}