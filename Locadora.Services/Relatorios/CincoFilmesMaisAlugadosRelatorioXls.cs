using Locadora.Domain.AggregatesModels.FilmeAggregate;
using Locadora.Data.Contexts;
using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Locadora.Services.Relatorios;

public class CincoFilmesMaisAlugadosRelatorioXls : IRelatorioXls
{
    readonly RelatorioContext _context;
    readonly string _connectionString;
    public CincoFilmesMaisAlugadosRelatorioXls(IConfiguration configuration )
    {
        //_context = context;
        _connectionString = configuration.GetConnectionString("DefaultConnection");

    }

    public ReportFile GerarRelatorio()
    {
        using System.Data.IDbConnection db = new MySql.Data.MySqlClient.MySqlConnection(_connectionString);

        var result = db.Query(
            @$"
select Filme.*, TotalAlugado
from (
		select 	Id_Filme, count(Id_Filme) as TotalAlugado 
        from 	locacao where Year(DataLocacao) = Year(CurDate())
		group 	by Id_Filme
		order 	by count(Id_Filme) desc
		limit 	5
	) as Filtro
	inner join Filme on Filtro.Id_Filme = Filme.Id
                    ");




        //var idsDosFilmesMaisLocados = _context.Locacoes
        //                .Where(x => x.DataLocacao.Year == DateTime.Now.Year)
        //                .GroupBy(x => x.IdFilme)
        //                .Select(x =>
        //                        new
        //                        {
        //                            IdFilme = x.Key,
        //                            QtdAlugado = x.Count()
        //                        })
        //                .OrderBy(x => x.QtdAlugado)
        //                .Take(5).ToArray();


        //List<Filme> cincoMaisAlugados = _context.Filmes
        //    .Where(x => idsDosFilmesMaisLocados.Any(a => a.IdFilme == x.Id))
        //    .ToList();





        StringBuilder builder = new StringBuilder("");
        builder.AppendLine($"Id \tTitulo \tAlugueis");
        builder.AppendLine(String.Join(
            Environment.NewLine,
            result.Select(x => $"{x.Id}\t{x.Titulo}\t{x.TotalAlugado}")
            ));

        return new ReportFile() { Content = builder.ToString(), MediaType = "application/vnd.ms-excel" };
    }
}
