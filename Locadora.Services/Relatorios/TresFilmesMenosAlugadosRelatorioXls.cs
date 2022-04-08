using Dapper;
using Locadora.Domain.AggregatesModels.FilmeAggregate;
using Locadora.Data.Contexts;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Locadora.Services.Relatorios;

public class TresFilmesMenosAlugadosRelatorioXls : IRelatorioXls
{    
    readonly string _connectionString;

    public TresFilmesMenosAlugadosRelatorioXls(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public ReportFile GerarRelatorio()
    {
        //long ticksDe7DiasAtras = DateTime.Now.AddDays(-7).Ticks;

        //var idsDosFilmesMaisLocados =
        //        _context.Locacoes
        //            .Where(x => x.DataLocacao.Ticks > ticksDe7DiasAtras)
        //            .GroupBy(x => x.IdFilme)
        //            .Select(x =>
        //                    new
        //                    {
        //                        IdFilme = x.Key,
        //                        QtdAlugado = x.Count()
        //                    })
        //            .OrderBy(x => x.QtdAlugado)
        //            .Take(3);


        //List<Filme> tresMenosAlugados = _context.Filmes
        //    .Where(x => idsDosFilmesMaisLocados.Any(a => a.IdFilme == x.Id))
        //    .ToList();


        using System.Data.IDbConnection db = new MySql.Data.MySqlClient.MySqlConnection(_connectionString);

        var result = db.Query(
            $@"
                    select 	Filme.Id, Filme.Titulo, count(*) as QtdLocacoes
                    from 	Locacao
                            inner join Filme on Filme.Id = Locacao.Id_Filme
                    group 	by Filme.Id, Filme.Titulo
                    order 	by count(Filme.Id)
                    limit 3;
                "
            );


        StringBuilder builder = new StringBuilder("");
        builder.AppendLine($"Id \tTitulo \tLocações");
        builder.AppendLine(String.Join(
            Environment.NewLine,
            result.Select(x => $"{x.Id}\t{x.Titulo}\t{x.QtdLocacoes}")
            ));

        return new ReportFile() { Content = builder.ToString(), MediaType = "application/vnd.ms-excel" };
    }
}
