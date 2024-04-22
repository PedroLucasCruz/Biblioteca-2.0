using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Extension.BaseValidacoes;

namespace Biblioteca._2._0.Domain.Interfaces.Service
{
    public interface IServicoUsuario
    {
        Usuario Cadastrar(Usuario obj);

        InconsistenciaDeValidacaoTipado<IList<Usuario>> ObtenhaTodosUsuarios();

        Usuario AtualizeUsuario(Usuario obj);

        InconsistenciaDeValidacao DeleteUsuario(int id);

        bool AutentiqueUsuario(Usuario obj);

        InconsistenciaDeValidacaoTipado<Usuario> ObterUsuarioId(int id);
    }
}
