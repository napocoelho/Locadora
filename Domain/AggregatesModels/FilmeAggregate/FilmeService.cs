using Locadora.Domain.AggregatesModels.FilmeAggregate;
using Locadora.Domain.AggregatesModels.LocacaoAggregate;


namespace Locadora.Domain.AggregatesModels.FilmeAggregate;

public class FilmeService : IFilmeService
{
    private readonly IFilmeRepository _filmeRepository;
    private readonly ILocacaoRepository _locacaoRepository;

    public FilmeService(IFilmeRepository filmeRepository, ILocacaoRepository locacaoRepository)
    {
        _filmeRepository = filmeRepository;
        _locacaoRepository = locacaoRepository;
    }

    public void Incluir(Filme item)
    {
        _filmeRepository.Incluir(item);
    }

    public void Alterar(Filme item)
    {
        _filmeRepository.Alterar(item);
    }

    public void Excluir(int idFilme)
    {
        if (!_locacaoRepository.FilmePossuiLocacao(idFilme))
        {
            _filmeRepository.Excluir(idFilme);
        }
        else
        {
            throw new Exception("O filme não pode ser excluído, pois possui locação");
        }
    }

    public IEnumerable<Filme> ObterTodos()
    {
        return _filmeRepository.ObterTodos();
    }

    public Filme? ObterPorId(int id)
    {
        return _filmeRepository.ObterPorId(id);
    }

    public Filme? ObterPorTitulo(string titulo)
    {
        return _filmeRepository.ObterPorTitulo(titulo);
    }

    public IEnumerable<Filme> ObterNaoAlugados()
    {
        return _filmeRepository.ObterNaoAlugados();
    }


    public IEnumerable<ItemDeImportacaoCsv> ImportarFilmes(Filme[] filmes)
    {
        //List<Filme> filmesImportados = new List<Filme>();
        List<ItemDeImportacaoCsv> listaDeRetorno = new List<ItemDeImportacaoCsv>();

        foreach (Filme filme in filmes)
        {
            ItemDeImportacaoCsv itemDeImportacao = new ItemDeImportacaoCsv();
            //itemDeImportacao.Filme = filme;
            listaDeRetorno.Add(itemDeImportacao);

            try
            {
                Filme filmeAtualizado = null;


                if (filme.Id is null)
                {
                    if (ObterPorTitulo(filme.Titulo) is not null)
                    {
                        throw new Exception($"Já existe um filme registrado com o título {filme.Titulo}");
                    }

                    Incluir(filme);
                }
                else
                {
                    if (ObterPorId(filme.Id ?? 0) is null)
                    {
                        if (ObterPorTitulo(filme.Titulo) is not null)
                        {
                            throw new Exception($"Já existe um filme registrado com o título '{filme.Titulo}'");
                        }

                        if (ObterPorId(filme.Id ?? 0) is not null)
                        {
                            throw new Exception($"Já existe um filme registrado com o id '{filme.Id}'");
                        }

                        Incluir(filme);
                    }
                    else
                    {
                        Filme? filmeTemp = ObterPorTitulo(filme.Titulo);

                        if (filmeTemp is not null && filmeTemp.Id != filme.Id)
                        {
                            throw new Exception($"Já existe um filme registrado com o título '{filme.Titulo}'");
                        }

                        Alterar(filme);
                    }
                }

                filmeAtualizado = ObterPorTitulo(filme.Titulo);

                if (filmeAtualizado is not null)
                {
                    itemDeImportacao.Filme = filmeAtualizado;
                }
            }
            catch (Exception ex)
            {
                itemDeImportacao.Erro = $"Houve uma falha ao registrar o filme '{filme.Titulo}': {ex.Message}";
            }
        }


        return listaDeRetorno;
    }
}