using Locadora.Domain.AggregatesModels.ClienteAggregate;
using Locadora.Domain.AggregatesModels.FilmeAggregate;
using Locadora.Domain.AggregatesModels.LocacaoAggregate;
using Locadora.Server.Mappers;
using Locadora.Shared.DTOs;
using Locadora.Shared.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Locadora.Server.Controllers;



[Route("api/[controller]")]
[ApiController]
public class LocacaoController : ControllerBase
{
    private ILocacaoService _locacaoService;
    private IClienteService _clienteService;
    private IFilmeService _filmeService;

    public LocacaoController(ILocacaoService locacaoService, IClienteService clienteService, IFilmeService filmeService)
    {
        ArgumentNullException.ThrowIfNull(locacaoService, nameof(locacaoService));

        _locacaoService = locacaoService;
        _clienteService = clienteService;
        _filmeService = filmeService;
    }

    // GET: api/<LocacaoController>
    [HttpGet]
    public IEnumerable<LocacaoDto> Get()
    {
        IEnumerable<Locacao> itens = _locacaoService.ObterTodos();

        //itens.Select(x => x.ToDto())
        //    .ToList()
        //    .ForEach(x =>
        //    {
        //        if (x is not null)
        //        {
        //            x.Filme = _filmeService.ObterPorId(x.IdFilme).ToDto();
        //            x.Cliente = _clienteService.ObterPorId(x.IdCliente).ToDto();
        //        }
        //    });

        return itens.ToDto()
                    .Select(x =>
                        {
                            PreencherLocacaoDto(x);
                            return x;
                        });

        // .Select(x => x.ToDto()) ?? Enumerable.Empty<LocacaoDto>();
    }

    [HttpGet("filmes_nao_alugados")]
    public FilmeDto[] Get_Filmes_Nao_Alugados()
    {
        List<Filme> itens = _filmeService.ObterNaoAlugados().ToList();
        return itens.ToDto().ToArray();
    }

    [HttpGet("clientes")]
    public ClienteDto[] Get_Clientes()
    {
        IEnumerable<Cliente> itens = _clienteService.ObterTodos();
        return itens.ToDto().ToArray();
    }

    [HttpGet("filmes")]
    public FilmeDto[] Get_Filmes()
    {
        IEnumerable<Filme> itens = _filmeService.ObterTodos();
        return itens.ToDto().ToArray();
    }

    // GET api/<LocacaoController>/5
    [HttpGet("{id}")]
    public LocacaoDto? Get(int id)
    {
        LocacaoDto? item = _locacaoService.ObterPorId(id).ToDto();
        PreencherLocacaoDto(item);
        return item;
    }



    // POST api/<LocacaoController>
    [HttpPost]
    public Result<LocacaoDto> Post([FromBody] LocacaoDto item)
    {
        try
        {
            Locacao? locacao = item.FromDto();

            if (locacao is null)
            {
                throw new Exception("Houve um problema ao preencher os dados de uma locação");
            }

            _locacaoService.Incluir(locacao);

            return Result<LocacaoDto>.Ok(locacao.ToDto());
        }
        catch (Exception ex)
        {
            return Result<LocacaoDto>.Fail(ex.Message);
        }
    }



    // PUT api/<LocacaoController>/5
    [HttpPut("devolver_filme/{idFilme}")]
    public Result<LocacaoDto> Put_Devolucao(int idFilme, /*[FromBody]*/ LocacaoDto item)
    {
        try
        {
            Locacao? locacaoEmAberto = _locacaoService.ObterLocacaoEmAbertoPorIdFilme(idFilme);

            if (locacaoEmAberto is null)
            {
                throw new Exception("O filme não está alugado");
            }

            _locacaoService.DevolverFilme(idFilme);
            Locacao? locacaoFinalizada = _locacaoService.ObterPorId(locacaoEmAberto?.Id ?? 0);

            LocacaoDto? retorno = locacaoFinalizada.ToDto();
            PreencherLocacaoDto(retorno);

            return Result<LocacaoDto>.Ok(retorno);
        }
        catch (Exception ex)
        {
            return Result<LocacaoDto>.Fail(ex.Message);
        }
    }

    // PUT api/<LocacaoController>/5
    [HttpPut("{id}")]
    public Result<LocacaoDto> Put(int id, [FromBody] LocacaoDto item)
    {
        try
        {
            Locacao? locacao = item.FromDto();

            if (locacao is null)
            {
                throw new Exception("Houve um problema ao preencher os dados da locação");
            }

            locacao.Id = id;
            _locacaoService.Alterar(locacao);


            LocacaoDto? retorno = locacao.ToDto();
            PreencherLocacaoDto(retorno);

            return Result<LocacaoDto>.Ok(retorno);
        }
        catch (Exception ex)
        {
            return Result<LocacaoDto>.Fail(ex.Message);
        }



    }

    // DELETE api/<LocacaoController>/5
    [HttpDelete("{id}")]
    public Result<LocacaoDto> Delete(int id)
    {


        try
        {
            Locacao? filme = _locacaoService.ObterPorId(id); ;

            if (filme is not null)
            {
                _locacaoService.Excluir(id);
                return Result<LocacaoDto>.Ok(filme.ToDto());
            }

            return Result<LocacaoDto>.Ok();
        }
        catch (Exception ex)
        {
            return Result<LocacaoDto>.Fail(ex.Message);
        }
    }

    private void PreencherLocacaoDto(LocacaoDto? item)
    {
        if (item is not null)
        {
            item.Filme = _filmeService.ObterPorId(item.IdFilme).ToDto();
            item.Cliente = _clienteService.ObterPorId(item.IdCliente).ToDto();
        }
    }
}
