using Microsoft.EntityFrameworkCore;
using Locadora.Domain.AggregatesModels.ClienteAggregate;



namespace Data.Contexts;



public class ClienteContext : DbContext
{
    public DbSet<Cliente> Clientes { get; set; }



    public ClienteContext(DbContextOptions<ClienteContext> options) : base(options)
    {
        
    }
}