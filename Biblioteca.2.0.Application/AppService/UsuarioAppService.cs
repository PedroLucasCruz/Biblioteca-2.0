using Biblioteca._2._0.Application.Dtos.Usuarios;
using Biblioteca._2._0.Application.IntefacesAppService;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Service;
using Biblioteca._2._0.Extension.BaseValidacoes;


namespace Biblioteca._2._0.Application.AppService
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IServicoUsuario _servicoUsuario;

        public UsuarioAppService(IServicoUsuario servicoUsuario)
        {
            _servicoUsuario = servicoUsuario;
        }

        public Usuario AtualizeUsuario(UsuarioDto obj)
        {           
            if(obj.ValideCadastro(obj.ObterEntidade()).EhValido());           

            return _servicoUsuario.AtualizeUsuario(obj.ObterEntidade());
        }

        public bool AutentiqueUsuario(UsuarioDto obj)
        {
            return _servicoUsuario.AutentiqueUsuario(obj.ObterEntidade());
        }

        public Usuario Cadastrar(UsuarioDto obj)
        {
            return _servicoUsuario.Cadastrar(obj.ObterEntidade());
        }

        public InconsistenciaDeValidacao DeleteUsuario(int Id)
        {
            return _servicoUsuario.DeleteUsuario(Id);
        }

        public InconsistenciaDeValidacaoTipado<IList<Usuario>> ObtenhaTodosUsuarios()
        {
            return _servicoUsuario.ObtenhaTodosUsuarios();
        }

        public InconsistenciaDeValidacaoTipado<Usuario> ObterUsuarioId(int id)
        {
          return _servicoUsuario.ObterUsuarioId(id);
        }
    }
}
