using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Repositories.Base;

namespace Biblioteca._2._0.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepositorio : IRepositorioGenerico<Usuario>
    {
        void CadastrarUsuarios(IList<Usuario> usuarios);
        bool VerifiqueAutenticidadeDoUsuario(Usuario obj);
    }
}
