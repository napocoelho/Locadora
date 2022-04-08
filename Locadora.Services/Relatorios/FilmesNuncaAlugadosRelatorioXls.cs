using Dapper;
using Locadora.Domain.AggregatesModels.FilmeAggregate;
using Locadora.Domain.AggregatesModels.LocacaoAggregate;
using Locadora.Data.Contexts;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Locadora.Services.Relatorios;

public class FilmesNuncaAlugadosRelatorioXls : IRelatorioXls
{
    readonly string _connectionString;

    public FilmesNuncaAlugadosRelatorioXls(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }



    public ReportFile GerarRelatorio()
    {
        //IEnumerable<Locacao> locacoes = _context.Locacoes;
        //List<Filme> filmesNuncaAlugados = _context.Filmes
        //            .Where(x => !locacoes.Any(a => a.IdFilme == x.Id))
        //            .ToList();

        using System.Data.IDbConnection db = new MySql.Data.MySqlClient.MySqlConnection(_connectionString);

        var result = db.Query(
            $@"
                    select * from Filme where Id not in ( select Id_Filme from Locacao );
                "
            );


        StringBuilder builder = new StringBuilder("");

        builder.AppendLine($"Id \tTitulo");
        builder.AppendLine(String.Join(
            Environment.NewLine,
            result.Select(x => $"{x.Id}\t{x.Titulo}")
            ));

        return new ReportFile() { Content = builder.ToString(), MediaType = "application/vnd.ms-excel" };
    }
}