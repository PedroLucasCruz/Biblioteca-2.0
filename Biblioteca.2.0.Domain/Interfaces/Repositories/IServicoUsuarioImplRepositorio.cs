using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca._2._0.Domain.Interfaces.Repositories
{
    public interface IServicoUsuarioImplRepositorio
    {
        bool LivroEmUso(int Id);
    }
}
