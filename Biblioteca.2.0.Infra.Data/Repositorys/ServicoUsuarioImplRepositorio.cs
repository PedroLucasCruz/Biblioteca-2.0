using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Repositories;
using Biblioteca._2._0.Domain.Interfaces.Service;
using Biblioteca._2._0.Extension.Utils;
using Biblioteca._2._0.Infra.Data.Context;
using Biblioteca._2._0.Infra.Data.Repositorys.Base;


namespace Biblioteca._2._0.Infra.Data.Repositorys
{
    public class ServicoUsuarioImplRepositorio : EFRepositorioGenerico<Usuario>, IUsuarioRepositorio
    {
        public ServicoUsuarioImplRepositorio(ApplicationDbContext contexto) : base(contexto)
        {

        }

        public void CadastrarUsuarios(IList<Usuario> usuarios)
        {
            foreach (var usuario in usuarios)
            {
                if (!_DbSet.Where(x => x.Codigo == usuario.Codigo || x.Nome == usuario.Nome).Any())
                {
                    _DbSet.Add(usuario);
                }
            }
            _contexto.SaveChanges();
        }

        public bool VerifiqueAutenticidadeDoUsuario(Usuario obj)
        {
            return _DbSet.Where(x => x.Nome.ToLowerInvariant() == obj.Nome.ToLowerInvariant() && x.Senha == UtilitarioDeCriptografia.Criptografe(obj.Senha)).Any();
        }

    }
}
