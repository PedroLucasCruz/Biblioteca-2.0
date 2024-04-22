using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Repositories;
using Biblioteca._2._0.Domain.Interfaces.Service;
using Biblioteca._2._0.Extension.BaseValidacoes;
using Biblioteca._2._0.Extension.Utils;


namespace Biblioteca._2._0.Domain.Services
{
    public class ServicoUsuarioImpl :  IServicoUsuario
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public ServicoUsuarioImpl(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public Usuario AtualizeUsuario(Usuario obj)
        {
            obj.Senha = UtilitarioDeCriptografia.Criptografe(obj.Senha);

            return _usuarioRepositorio.Altere(obj);
        }

        public bool AutentiqueUsuario(Usuario obj)
        {
            return _usuarioRepositorio.ObtenhaTodos().Where(x => x.Nome.ToLowerInvariant() == obj.Nome.ToLowerInvariant() && x.Senha == UtilitarioDeCriptografia.Criptografe(obj.Senha)).Any();
        }

        public Usuario Cadastrar(Usuario obj)
        {
            return _usuarioRepositorio.Cadastre(obj);
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
            catch(Exception ex)
            {
                return new InconsistenciaDeValidacao { Mensagem = $"Erro ao deletar o usuario." + ex };
            }
        }

        public InconsistenciaDeValidacaoTipado<IList<Usuario>> ObtenhaTodosUsuarios()
        {
            var retorno = _usuarioRepositorio.ObtenhaTodos();

            if(retorno.PossuiValor()) return new InconsistenciaDeValidacaoTipado<IList<Usuario>>() { ObjetoRetorno = retorno, Mensagem = "Dados encontrados com sucesso!" };

            return new InconsistenciaDeValidacaoTipado<IList<Usuario>> () { ObjetoRetorno = retorno, Mensagem = "Erro ao localizar dados" };
        }

        public InconsistenciaDeValidacaoTipado<Usuario> ObterUsuarioId(int id)
        {
          var retorno =  _usuarioRepositorio.ObtenhaPorId(id);

          if (retorno.PossuiValor()) return new InconsistenciaDeValidacaoTipado<Usuario>() { ObjetoRetorno = retorno, Mensagem = "Usuario encontrado com sucesso" };

          return new InconsistenciaDeValidacaoTipado<Usuario>() { ObjetoRetorno = retorno, Mensagem = "Falha ao localizar usuario" };         
        }
    }
}
