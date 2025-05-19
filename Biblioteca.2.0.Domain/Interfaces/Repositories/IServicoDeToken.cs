using Biblioteca._2._0.Domain.Entidades;

namespace Biblioteca._2._0.Infra.Data.JWT.Repositories
{
    public interface IServicoDeToken
    {
        string GerarToken(Usuario usuario);

        bool ValidarToken(string token);

        string RenoveToken(string token);
    }
}
