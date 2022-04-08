using Locadora.Services.Relatorios;
using Locadora.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Locadora.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RelatorioController : ControllerBase
{
    readonly IRelatoriosXlsService _service;



    public RelatorioController(IRelatoriosXlsService service)
    {
        _service = service;

    }

    //[HttpGet]
    //public string Get()
    //{
    //    return "!teste!";
    //}

    // https://localhost:7181/api/relatorio/cinco_filmes_mais_alugados



    [HttpGet("cinco_filmes_mais_alugados.xls")]
    public FileResult CincoFilmesMaisAlugados()
    {
        HttpResponseMessage result = null;
        try
        {
            ReportFile report = _service.CincoFilmesMaisAlugados().GerarRelatorio();

            result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(report.Content, System.Text.Encoding.Default, report.MediaType); //  new ByteArrayContent(bytes);                
                                                                                                                //result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                                                                                                                //result.Content.Headers.ContentDisposition.FileName = "Cinco_filmes_mais_alugados.xsl";

            Stream stream = result.Content.ReadAsStream();
            return File(stream, report.MediaType);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [HttpGet("segundo_cliente_que_mais_alugou.xls")]
    public FileResult SegundoClienteQueMaisAlugou()
    {
        HttpResponseMessage result = null;
        try
        {
            ReportFile report = _service.SegundoClienteQueMaisAlugou().GerarRelatorio();

            result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(report.Content, System.Text.Encoding.Default, report.MediaType);
            //result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            //result.Content.Headers.ContentDisposition.FileName = "segundo_cliente_que_mais_alugou.xsl";

            Stream stream = result.Content.ReadAsStream();
            return File(stream, report.MediaType);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [HttpGet("clientes_em_atraso_na_devolucao.xls")]
    public FileResult ClientesEmAtrasoNaDevolucao()
    {
        HttpResponseMessage result = null;
        try
        {
            ReportFile report = _service.ClientesEmAtrasoNaDevolucao().GerarRelatorio();

            result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(report.Content, System.Text.Encoding.Default, report.MediaType);
            //result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            //result.Content.Headers.ContentDisposition.FileName = "clientes_em_atraso_na_devolucao.xsl";

            Stream stream = result.Content.ReadAsStream();
            return File(stream, report.MediaType);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [HttpGet("tres_filmes_menos_alugados.xls")]
    public FileResult TresFilmesMenosAlugados()
    {
        HttpResponseMessage result = null;
        try
        {
            ReportFile report = _service.TresFilmesMenosAlugados().GerarRelatorio();

            result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(report.Content, System.Text.Encoding.Default, report.MediaType);
            //result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            //result.Content.Headers.ContentDisposition.FileName = "tres_filmes_menos_alugados.xsl";

            Stream stream = result.Content.ReadAsStream();
            return File(stream, report.MediaType);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    [HttpGet("filmes_nunca_alugados.xls")]
    public FileResult FilmesNuncaAlugados()
    {
        HttpResponseMessage result = null;
        try
        {
            ReportFile report = _service.FilmesNuncaAlugados().GerarRelatorio();

            result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(report.Content, System.Text.Encoding.Default, report.MediaType);
            //result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            //result.Content.Headers.ContentDisposition.FileName = "filmes_nunca_alugados.xsl";

            Stream stream = result.Content.ReadAsStream();
            return File(stream, report.MediaType);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}
