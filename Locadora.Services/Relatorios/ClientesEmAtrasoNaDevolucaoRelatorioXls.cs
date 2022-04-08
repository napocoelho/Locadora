using Dapper;
using Locadora.Domain.AggregatesModels.ClienteAggregate;
using Locadora.Domain.AggregatesModels.LocacaoAggregate;
using Locadora.Data.Contexts;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Locadora.Services.Relatorios;

public class ClientesEmAtrasoNaDevolucaoRelatorioXls : IRelatorioXls
{
    readonly string _connectionString;

    public ClientesEmAtrasoNaDevolucaoRelatorioXls(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");

    }

    public ReportFile GerarRelatorio()
    {
        //IEnumerable<Locacao> locacoesAtrasadas = _context.Locacoes.Where(x => x.DataDevolucao == null);
        //List<Cliente> clientesComAtraso = _context.Clientes
        //            .Where(x => locacoesAtrasadas.Any(a => a.IdCliente == x.Id))
        //            .ToList();


        using System.Data.IDbConnection db = new MySql.Data.MySqlClient.MySqlConnection(_connectionString);

        var result = db.Query(
            $@"
select 	Cliente.*, DataLocacao, date_add( DataLocacao, INTERVAL if(Filme.Lancamento=1, 2, 3) DAY ) as DataDevolucaoAtrasada
from 	Locacao 
		inner join Filme on Filme.Id = Locacao.Id_Filme
        inner join Cliente on Cliente.Id = Locacao.Id_Cliente
where 	DataDevolucao is null 
		and date_add( DataLocacao, INTERVAL if(Filme.Lancamento=1, 2, 3) DAY ) > curdate()
                "
            );

        StringBuilder builder = new StringBuilder("");
        string[] titulos = new string[] { "Id", "Nome", "Data locação", "Data devolução" };
        builder.AppendLine(string.Join('\t', titulos));
        builder.AppendLine(String.Join(
            Environment.NewLine,
            result.Select(x => $"{x.Id}\t{x.Nome}\t{x.DataLocacao}\t{x.DataDevolucao}")
            ));

        return new ReportFile() { Content = builder.ToString(), MediaType = "application/vnd.ms-excel" };
    }
}
