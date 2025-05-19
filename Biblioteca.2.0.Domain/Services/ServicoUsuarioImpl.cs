using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Repositories;
using Biblioteca._2._0.Domain.Interfaces.Service;
using Biblioteca._2._0.Domain.ValidacoesNegocio.Usuarios;
using Biblioteca._2._0.Extension.BaseValidacoes;
using Biblioteca._2._0.Extension.Utils;
using Biblioteca._2._0.Infra.Data.JWT.Repositories;
using Biblioteca.Negocio.Validacoes.Usuarios;
using Microsoft.Extensions.Logging;


namespace Biblioteca._2._0.Domain.Services
{
    public class ServicoUsuarioImpl : IServicoUsuario
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        protected ILogger<ServicoUsuarioImpl> _logger;
        private readonly IServicoDeToken _servicoDeToken;

        public ServicoUsuarioImpl(ILogger<ServicoUsuarioImpl> logger, IUsuarioRepositorio usuarioRepositorio)
        {
            _logger = logger;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public Usuario AtualizeUsuario(Usuario obj)
        {
            obj.Senha = UtilitarioDeCriptografia.Criptografe(obj.Senha);

            return _usuarioRepositorio.Altere(obj);
        }

        public InconsistenciaDeValidacaoTipado<Usuario> AutentiqueUsuario(Usuario? obj)
        {
            try
            {
                Usuario usuarioLogado = null;
                string token = "";

                _logger.LogInformation("Iniciando Autenticação do Usuário");

                usuarioLogado = _usuarioRepositorio?.ObtenhaTodos()?.Where(x => x?.Nome?.ToLowerInvariant() == obj?.Nome?.ToLowerInvariant()).FirstOrDefault() ?? throw new("Erro ao localizar dados");

                _logger.LogInformation("Autenticação do Usuário Executada - Gerando Token");

                token = _servicoDeToken.GerarToken(usuarioLogado);

                _logger.LogInformation($"Token Gerado = {token}");

                _logger.LogInformation("Usuario logado com sucesso");
                return new InconsistenciaDeValidacaoTipado<Usuario>() { ObjetoRetorno = usuarioLogado, Mensagem = "Login Feito com sucesso" };

            }
            catch
            {
                throw new Exception("Erro ao autenticar");
            } 
        }


        public InconsistenciaDeValidacaoTipado<Usuario> Cadastrar(Usuario obj)
        {

            Usuario novoCadastro = null;
            _logger.LogInformation("Iniciando Validação do Usuário");
            var validador = new AutenticacaoValidador();
            var validacoes = validador.ValideCadastro(obj);

            _logger.LogInformation("Iniciando Validação do Usuário Impeditivas");
            var validadorImpeditivas = new ServicoUsuarioValidador(_usuarioRepositorio);
            var validacoesImpeditivas = validadorImpeditivas.ValideCadastroImpeditivo(obj);


            if (validacoes.EhValido() && validacoesImpeditivas.EhValido())
            {
                _logger.LogInformation("Iniciando Cadastro do Usuário");

                _logger.LogInformation("Criptografando a Senha do Cadastro do Usuário");
                obj.Senha = UtilitarioDeCriptografia.Criptografe(obj.Senha);

                _logger.LogInformation("Criando Código Guid do Cadastro do Usuário");
                obj.Codigo = Guid.NewGuid();

                _logger.LogInformation("Gravando Cadastro do Usuário");
                novoCadastro = _usuarioRepositorio.Cadastre(obj);              
            }
            else
            {
                _logger.LogInformation("Existe Inconsistências");
              
            }

                _logger.LogInformation("Retornando Cadastro do Usuário");
        


            return new InconsistenciaDeValidacaoTipado<Usuario>() { ObjetoRetorno = novoCadastro, Mensagem = "Login Feito com sucesso" };
        }

        public void CadastrarUsuarios(IList<Usuario> usuarios)
        {
            if (!usuarios.PossuiValor() || !usuarios.PossuiLinhas())
            {
                return;
            }

            var usuarioscadastrados = _usuarioRepositorio.ObtenhaTodos();

            if (usuarioscadastrados.PossuiValor() && usuarioscadastrados.PossuiLinhas()) return;

            foreach (var usuario in usuarios)
            {
                _usuarioRepositorio.Cadastre(usuario);
            }
        }


        public InconsistenciaDeValidacao DeleteUsuario(int Id)
        {
            try
            {
                var usuario = _usuarioRepositorio.ObtenhaPorId(Id);

                if (usuario.PossuiValor())
                    _usuarioRepositorio.Delete(Id);

                return new InconsistenciaDeValidacao { Mensagem = $"Usuario deletado com sucesso" };
            }
            catch (Exception ex)
            {
                return new InconsistenciaDeValidacao { Mensagem = $"Erro ao deletar o usuario." + ex };
            }
        }

        public InconsistenciaDeValidacaoTipado<IList<Usuario>> ObtenhaTodosUsuarios()
        {
            var retorno = _usuarioRepositorio.ObtenhaTodos();

            if (retorno.PossuiValor()) return new InconsistenciaDeValidacaoTipado<IList<Usuario>>() { ObjetoRetorno = retorno, Mensagem = "Dados encontrados com sucesso!" };

            return new InconsistenciaDeValidacaoTipado<IList<Usuario>>() { ObjetoRetorno = retorno, Mensagem = "Erro ao localizar dados" };
        }

        public InconsistenciaDeValidacaoTipado<Usuario> ObterUsuarioId(int id)
        {
            var retorno = _usuarioRepositorio.ObtenhaPorId(id);

            if (retorno.PossuiValor()) return new InconsistenciaDeValidacaoTipado<Usuario>() { ObjetoRetorno = retorno, Mensagem = "Usuario encontrado com sucesso" };

            return new InconsistenciaDeValidacaoTipado<Usuario>() { ObjetoRetorno = retorno, Mensagem = "Falha ao localizar usuario" };
        }

        public string RenoveToken(string token)
        {

          return  _servicoDeToken.RenoveToken(token);
                       
        }
    } 
}