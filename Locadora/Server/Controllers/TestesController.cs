using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

using Newtonsoft;


namespace Locadora.Server.Controllers;



[Route("api/[controller]")]
[ApiController]
public class TestesController : ControllerBase
{
    [HttpGet("teste")]
    public IEnumerable<ClienteXX> Teste()
    {
        IEnumerable<ClienteXX> clientes = new List<ClienteXX>();

        using (var connection = new MySqlConnection("Server=localhost;Database=testes;User Id=root;Password=jurubeba;"))
        {
            clientes = connection.Query<ClienteXX>("SELECT * FROM Cliente");
        }
        return clientes.ToArray();
    }


    /// <summary>
    /// Retorna um teste
    /// </summary>
    /// <returns></returns>
    [HttpGet("show_page")]
    public IEnumerable<string> ShowPage()
    {

        return new string[] { "junio", "coelho" };
    }


    [HttpPost]
    public void Post([FromBody] int teste)
    {
        throw new Exception($"O valor de teste foi {teste}");
    }
}







public class ClienteXX
{
    public int? Id { get; set; }
    public string? Nome { get; set; }
    public string? CPF { get; set; }
    public DateTime? DataNascimento { get; set; }
}