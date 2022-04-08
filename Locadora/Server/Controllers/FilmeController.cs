using Locadora.Domain.AggregatesModels.FilmeAggregate;
using Locadora.Server.Mappers;
using Locadora.Shared.DTOs;
using Locadora.Shared.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Locadora.Server.Controllers;



[Route("api/[controller]")]
[ApiController]
public class FilmeController : ControllerBase
{
    IFilmeService _service;

    public FilmeController(IFilmeService service)
    {
        _service = service;
    }

    // GET: api/<FilmeController>
    [HttpGet]
    public FilmeDto[] Get()
    {
        try
        {
            IEnumerable<Filme> itens = _service.ObterTodos();
            FilmeDto[] filmes = (
                                    itens?.Select(x => x.ToDto())
                                        ?? Enumerable.Empty<FilmeDto>()
                                 ).ToArray();
            return filmes;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    // GET api/<FilmeController>/5
    [HttpGet("{id}")]
    public Result<FilmeDto?> Get(int id)
    {
        try
        {
            Filme item = _service.ObterPorId(id);
            return Result<FilmeDto>.Ok(item.ToDto());
        }
        catch (Exception ex)
        {
            return Result<FilmeDto>.Fail(ex.Message);
        }


    }

    // POST api/<FilmeController>
    [HttpPost]
    public Result<FilmeDto> Post([FromBody] FilmeDto item)
    {
        try
        {
            _service.Incluir(item.FromDto());
            FilmeDto? filme = _service.ObterPorTitulo(item.Titulo).ToDto();
            return Result<FilmeDto>.Ok(filme);
        }
        catch (Exception ex)
        {
            return Result<FilmeDto>.Fail(ex.Message);
        }
    }

    // PUT api/<FilmeController>/5
    [HttpPut("{id}")]
    public Result<FilmeDto> Put(int id, [FromBody] FilmeDto item)
    {
        try
        {

            Filme filme = item.FromDto();
            filme.Id = id;
            _service.Alterar(filme);

            FilmeDto? retorno = _service.ObterPorTitulo(item.Titulo).ToDto();

            return Result<FilmeDto>.Ok(retorno);
        }
        catch (Exception ex)
        {
            return Result<FilmeDto>.Fail(ex.Message);
        }
    }

    // DELETE api/<FilmeController>/5
    [HttpDelete("{id}")]
    public Result<FilmeDto> Delete(int id)
    {
        try
        {
            Filme? filme = _service.ObterPorId(id); ;

            if (filme is not null)
            {
                _service.Excluir(id);
                return Result<FilmeDto>.Ok(filme.ToDto());
            }

            return Result<FilmeDto>.Ok();
        }
        catch (Exception ex)
        {
            return Result<FilmeDto>.Fail(ex.Message);
        }
    }

    [HttpPost("importar_csv")]
    public Result<ItemDeImportacaoCsvDto[]> PostImportarCsv([FromBody] FileContentDto file)
    {
        List<string> erros = new List<string>();


        IEnumerable<Filme> filmesDesserializados = null;

        try
        {
            filmesDesserializados = DesserializarCsv(file.Content);
        }
        catch (Exception ex)
        {
            return Result<ItemDeImportacaoCsvDto[]>.Fail(ex.Message);
        }

        IEnumerable<ItemDeImportacaoCsv> importacoes = _service.ImportarFilmes(filmesDesserializados.ToArray());


        return Result<ItemDeImportacaoCsvDto[]>.Ok(importacoes.ToDto().ToArray());
    }






    private IEnumerable<Filme> DesserializarCsv(string fileContent)
    {

        List<Filme> filmesDesserializados = new List<Filme>();

        string[] lines = fileContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                                     .Skip(1).ToArray();


        int numeroLinha = 0;

        foreach (string line in lines)
        {
            string[] columns = line.Split(";");
            numeroLinha++;


            if (columns.Length == 4)
            {
                try
                {
                    int id = 0;
                    int.TryParse(columns[0], out id);

                    string titulo = columns[1].Replace("  ", " ").Trim();

                    int classificacao = 0;
                    int.TryParse(columns[2], out classificacao);

                    int lancamento = 0;
                    int.TryParse(columns[3], out lancamento);

                    Filme filme = new Filme(id == 0 ? null : id, titulo, classificacao, lancamento > 0);
                    filmesDesserializados.Add(filme);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Houve um erro ao desserializar arquivo .csv. O erro foi encontrado na linha {numeroLinha}. Mensagem: {ex.Message}.");
                }
            }
            else
            {
                throw new Exception($"Houve um erro ao desserializar arquivo .csv. O erro foi encontrado na linha {numeroLinha}. Motivo: o número de colunas é diferente de 4.");
            }

        }

        return filmesDesserializados;
    }
}