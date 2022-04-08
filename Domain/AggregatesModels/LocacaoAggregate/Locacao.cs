using Locadora.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Locadora.Domain.AggregatesModels.LocacaoAggregate;


[Table("Locacao")]
public class Locacao : IEntity, IAggregateRoot
{
    public int? Id { get; set; }
    public DateTime DataLocacao { get; set; }
    public DateTime? DataDevolucao { get; set; }


    [Column("Id_Cliente")]
    public int IdCliente { get; set; }

    [Column("Id_Filme")]
    public int IdFilme { get; set; }






    public Locacao(int? id, DateTime dataLocacao, DateTime? dataDevolucao, int idCliente, int idFilme)
    {
        ArgumentNullException.ThrowIfNull(dataLocacao, nameof(dataLocacao));
        ArgumentNullException.ThrowIfNull(idCliente, nameof(idCliente));
        ArgumentNullException.ThrowIfNull(idFilme, nameof(idFilme));

        Id = id;
        DataLocacao = dataLocacao;
        DataDevolucao = dataDevolucao;

        IdCliente = idCliente;
        IdFilme = idFilme;
    }
}