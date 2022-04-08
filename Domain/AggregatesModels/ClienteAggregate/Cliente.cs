using Locadora.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Domain.AggregatesModels.ClienteAggregate;

[Table("Cliente")]
public class Cliente :  IEntity, IAggregateRoot
{
    public int? Id { get; set; }

    public string Nome { get; set; }
    public string Cpf { get; set; }
    public DateTime DataNascimento { get; set; }

    


    public Cliente(int? id, string nome, string cpf, DateTime dataNascimento)
    {
        ArgumentNullException.ThrowIfNull(nome, nameof(nome));
        ArgumentNullException.ThrowIfNull(cpf, nameof(cpf));
        ArgumentNullException.ThrowIfNull(dataNascimento, nameof(dataNascimento));

        Id = id;
        Nome = nome;
        Cpf = cpf;
        DataNascimento = dataNascimento;
    }

    

    //public void Incluir(Cliente cliente)
    //{
    //    ArgumentNullException.ThrowIfNull(Nome, nameof(Nome));
    //    ArgumentNullException.ThrowIfNull(CPF, nameof(CPF));
        
    //    _repository.Incluir(this);
    //}

    //public void Alterar()
    //{
    //    ArgumentNullException.ThrowIfNull(Id, nameof(Id));
    //    ArgumentNullException.ThrowIfNull(Nome, nameof(Nome));
    //    ArgumentNullException.ThrowIfNull(CPF, nameof(CPF));
                
    //    _repository.Alterar(this);
    //}

    //public void Excluir()
    //{
    //    if (Id is not null)
    //    {
    //        _repository.

    //        if (_repository)
    //            _repository.Excluir(Id.Value);
    //    }
    //}
}