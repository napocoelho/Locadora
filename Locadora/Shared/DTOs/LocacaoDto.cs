using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Shared.DTOs
{
    public  class LocacaoDto
    {
        public int? Id { get; set; }
        public int IdCliente { get; set; }
        public int IdFilme { get; set; }


        public FilmeDto? Filme { get; set; }
        public ClienteDto? Cliente { get; set; }


        public DateTime DataLocacao { get; set; }
        public DateTime? DataDevolucao { get; set; }
    }
}
