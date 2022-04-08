
using Microsoft.AspNetCore.Mvc;


using Locadora.Server.Mappers;
using Locadora.Shared.DTOs;
using Locadora.Domain.AggregatesModels.ClienteAggregate;
using Locadora.Shared.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Locadora.Server.Controllers;



[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private IClienteService _service;

    public ClienteController(IClienteService service)
    {
        _service = service;
    }

    // GET: api/<ClienteController>
    [HttpGet]
    public ClienteDto[] Get()
    {
        //List<ClienteDto> lista = new List<ClienteDto>();
        //lista.Add(new ClienteDto() { Id = 1, Nome = "Junio Coelho", CPF = "000000", DataNascimento = DateTime.Parse("09/11/1982") });
        //lista.Add(new ClienteDto() { Id = 2, Nome = "Maurício Coelho", CPF = "100101", DataNascimento = DateTime.Parse("27/09/1973") });
        //return Ok(lista.ToArray());

        IEnumerable<ClienteDto> retorno = _service.ObterTodos().ToArray().Select(x => ClienteMapper.ToDto(x))
                                                ?? Enumerable.Empty<ClienteDto>();
        return retorno.ToArray();
    }

    [HttpGet("teste")]
    public Result<ClienteDto> Teste()
    {
        //List<ClienteDto> lista = new List<ClienteDto>();
        //lista.Add(new ClienteDto() { Id = 1, Nome = "Junio Coelho", CPF = "000000", DataNascimento = DateTime.Parse("09/11/1982") });
        //lista.Add(new ClienteDto() { Id = 2, Nome = "Maurício Coelho", CPF = "100101", DataNascimento = DateTime.Parse("27/09/1973") });
        //return Ok(lista.ToArray());


        return Result<ClienteDto>.Fail("paranauê paranauê paranauê");
    }


    // GET api/<ClienteController>/5
    [HttpGet("{id}")]
    public ClienteDto? Get(int id)
    {

        Cliente? retorno = _service.ObterPorId(id);

        if (retorno is null)
        {
            return null;
        }

        return ClienteMapper.ToDto(retorno);
    }

    // POST api/<ClienteController>
    [HttpPost]
    public Result<ClienteDto> Post([FromBody] Cliente valor)
    {
        try
        {
            _service.Inserir(valor);
            ClienteDto? retorno = _service.ObterPorCpf(valor.Cpf).ToDto();

            return Result<ClienteDto>.Ok(retorno);
        }
        catch (Exception ex)
        {
            return Result<ClienteDto>.Fail(ex.Message);
        }
    }



    // PUT api/<ClienteController>/5
    [HttpPut("{id}")]
    public Result<ClienteDto> Put(int id, [FromBody] ClienteDto item)
    {
        try
        {

            Cliente cliente = item.FromDto();
            cliente.Id = id;
            _service.Alterar(cliente);

            ClienteDto? retorno = _service.ObterPorCpf(item.CPF).ToDto();

            return Result<ClienteDto>.Ok(retorno);
        }
        catch (Exception ex)
        {
            return Result<ClienteDto>.Fail(ex.Message);
        }


    }

    // DELETE api/<ClienteController>/5
    [HttpDelete("{id}")]
    public Result<ClienteDto> Delete(int id)
    {
        try
        {
            Cliente? cliente = _service.ObterPorId(id); ;

            if (cliente is not null)
            {
                _service.Excluir(id);
                return Result<ClienteDto>.Ok(cliente.ToDto());
            }

            return Result<ClienteDto>.Ok();
        }
        catch (Exception ex)
        {
            return Result<ClienteDto>.Fail(ex.Message);
        }
    }
}


