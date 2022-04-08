using Dapper;
using Locadora.Domain.AggregatesModels.ClienteAggregate;
using Locadora.Data.Contexts;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Locadora.Services.Relatorios;

public class SegundoClienteQueMaisAlugouRelatorioXls : IRelatorioXls
{
    readonly string _connectionString;

    public SegundoClienteQueMaisAlugouRelatorioXls(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public ReportFile GerarRelatorio()
    {
        //var idsDosClientesQueMaisAlugaram =
        //        _context.Locacoes
        //            .GroupBy(x => x.IdCliente)
        //            .Select(x =>
        //                    new
        //                    {
        //                        IdCliente = x.Key,
        //                        QtdAlugado = x.Count()
        //                    })
        //            .OrderByDescending(x => x.QtdAlugado)
        //            .Take(2)
        //            .Skip(1);


        //List<Cliente> segundoCliente = _context.Clientes
        //    .Where(x => idsDosClientesQueMaisAlugaram
        //    .Any(a => a.IdCliente == x.Id))
        //    .ToList();

        using System.Data.IDbConnection db = new MySql.Data.MySqlClient.MySqlConnection(_connectionString);

        var result = db.Query(
            $@"
                    select 	Cliente.Id, Cliente.Nome, count(*) as QtdLocacoes
                    from 	Locacao
                            inner join Cliente on Cliente.Id = Locacao.Id_Cliente
                    group 	by Cliente.Id
                    order 	by count(Cliente.Id) desc
                    limit 1, 1;
                "
            );



        StringBuilder builder = new StringBuilder("");
        builder.AppendLine($"Id \tNome \tLocações");
        builder.AppendLine(String.Join(
            Environment.NewLine,
            result.Select(x => $"{x.Id}\t{x.Nome}\t{x.QtdLocacoes}")
            ));

        return new ReportFile() { Content = builder.ToString(), MediaType = "application/vnd.ms-excel" };
    }
}