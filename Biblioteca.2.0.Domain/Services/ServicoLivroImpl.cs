using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Repositories;
using Biblioteca._2._0.Domain.Interfaces.Service;
using Biblioteca._2._0.Domain.ValidacoesNegocio.Livros;
using Biblioteca._2._0.Extension.BaseValidacoes;
using Biblioteca._2._0.Extension.Utils;
using Microsoft.Extensions.Logging;


namespace Biblioteca._2._0.Domain.Services
{
    public class ServicoLivroImpl : IServicoLivro
    {

        private ILogger<ServicoLivroImpl> _logger;
        private readonly ILivroRepositorio _livroRepositorio;
        private readonly IServicoUsuarioImplRepositorio _servicoUsuarioImplRepositorio; 
        public ServicoLivroImpl(ILogger<ServicoLivroImpl> logger, ILivroRepositorio livroRepositorio, IServicoUsuarioImplRepositorio servicoUsuarioImplRepositorio)
        {
            _logger = logger;
            _livroRepositorio = livroRepositorio;
            _servicoUsuarioImplRepositorio = servicoUsuarioImplRepositorio;
        }

        public InconsistenciaDeValidacaoTipado<Livro> Atualizar(Livro livro)
        {
            Livro livroAtualizado = null;

            _logger.LogInformation("Serviço 'Serviço de Livro': Executando ajuste nos dados para atualização.");

            var entidade = _livroRepositorio.ObtenhaPorId(livro.Id); 
       
            entidade.DataAtualizacao = DateTime.Now;
      
            try
            {
                _logger.LogInformation("Serviço 'Serviço de Livro': Executando a atualização no banco de dados.");
                _livroRepositorio.Altere(entidade);

                livroAtualizado = _livroRepositorio.ObtenhaPorId(livro.Id);

                _logger.LogInformation("Serviço 'Serviço de Livro': Devolvendo o Resultado.");
                return new InconsistenciaDeValidacaoTipado<Livro>() { ObjetoRetorno = livroAtualizado, Mensagem = "Atualizado com Sucesso" };
            }
            catch (Exception ex)
            {
                _logger.LogError("Serviço 'Serviço de Livro': Erro ao atualizar o Livro.", ex);
                return new InconsistenciaDeValidacaoTipado<Livro>() { ObjetoRetorno = null, Mensagem = "Erro na atualização do Livro" };
            }
        }

        public InconsistenciaDeValidacaoTipado<Livro> Cadastrar(Livro livro)
        {
            livro.Codigo = Guid.NewGuid();
            livro.DataCriacao = DateTime.Now;
            livro.DataAtualizacao = DateTime.Now;

            _logger.LogInformation("Serviço 'Serviço de Livro': Início da Validação do Livro");

            var validacoesIniciais = new ServicoLivroValidador().ValideInicial(livro);
            if (!validacoesIniciais.EhValido()) return validacoesIniciais;
                                   

            try
            {

                _logger.LogInformation("Serviço 'Serviço de Livro': Executando o cadsatro.");
                var cadastroAtualizado =  _livroRepositorio.Cadastre(livro);
                

                _logger.LogInformation("Serviço 'Serviço de Livro': Devolvendo o resultado.");
                return new InconsistenciaDeValidacaoTipado<Livro>() { ObjetoRetorno = cadastroAtualizado, Mensagem = "Cadastrado com Sucesso" };
            }
            catch (Exception ex)
            {
                _logger.LogError("Serviço 'Serviço de Livro': Erro ao cadastrar o Livro.", ex);
                return new InconsistenciaDeValidacaoTipado<Livro>() { ObjetoRetorno = null, Mensagem = "Erro no cadastro do Livro" };
            }
        }

        public void CadastrarLivros(IList<Livro> livros)
        {
            CadastrarLivros(livros);
        }

        public Livro ConsultarLivroPorIdEditar(int idEditora)
        {
            throw new NotImplementedException();
        }

        public InconsistenciaDeValidacaoTipado<Livro> Deletar(int Id)
        {

            _logger.LogInformation("Serviço 'Serviço de Livro': Iniciando busca por possíveis relacionamentos do Livro");
                       
            var  livroEmUso = _servicoUsuarioImplRepositorio.LivroEmUso(Id);           

            if (!livroEmUso)
            {
                try
                {
                    _logger.LogInformation("Serviço 'Serviço de Livro': Iniciando a remoção do Livro");
                    _livroRepositorio.Delete(Id);
                    _logger.LogInformation("Serviço 'Serviço de Livro': Livro removido.");
                    return new InconsistenciaDeValidacaoTipado<Livro>() { Mensagem = $"Livro de código {Id.ToString("0000000")} foi deletado com sucesso" };
                }
                catch (Exception ex)
                {

                    _logger.LogError("Serviço 'Serviço de Livro': Livro não removido.", ex);
                    return new InconsistenciaDeValidacaoTipado<Livro>() { Mensagem = $"Livro de código {Id.ToString("0000000")} não deletado com sucesso, erro no sistema" };
                }

            }

            _logger.LogInformation("Serviço 'Serviço de Livro': Livro está em uso por alguma ficha de emprestimo.");
            return new InconsistenciaDeValidacaoTipado<Livro>() { Mensagem = $"Livro de código {Id.ToString("0000000")} não foi deletado com sucesso, pois está relacionado a Ficha de Alunos" };
        }

        public Livro ObtenhaLivro(int Id)
        {
            try
            {
                _logger.LogInformation("Serviço 'Serviço de Livro': Iniciando busca do Livro");
                var resultado = _livroRepositorio.ObtenhaPorId(Id);
                if (!resultado.PossuiValor())
                {
                    _logger.LogInformation("Serviço 'Serviço de Livro': Livro não encontrado");
                    return null;
                }

                _logger.LogInformation("Serviço 'Serviço de Livro': Fim busca do Livro, devolvendo resultado");
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError("Serviço 'Serviço de Livro': Busca do Livro apresentou erro", ex);
                return null;
            }

        }

        public IList<Livro> ObtenhaTodosLivros()
        {
            try
            {
                _logger.LogInformation("Serviço 'Serviço de Livro': Iniciando busca dos Livros");
                IList<Livro> livros = new List<Livro>();

                var resultado = _livroRepositorio.RetornarLivrosComAutores();

                if (!resultado.PossuiValor() && !resultado.PossuiLinhas())
                {
                    _logger.LogInformation("Serviço 'Serviço de Livro': Livros não encontrados");
                    return null;
                }

                _logger.LogInformation("Serviço 'Serviço de Livro': Fim busca do Livros, devolvendo resultados");
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError("Serviço 'Serviço de Livro': Busca dos Livros apresentou erro", ex);
                return null;
            }
        }
    }
}
