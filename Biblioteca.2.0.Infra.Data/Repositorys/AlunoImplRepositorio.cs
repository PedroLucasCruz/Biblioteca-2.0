using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Repositories;
using Biblioteca._2._0.Infra.Data.Context;
using Biblioteca._2._0.Infra.Data.Repositorys.Base;


namespace Biblioteca._2._0.Infra.Data.Repositorys
{
    internal class AlunoImplRepositorio : EFRepositorioGenerico<Aluno>, IAlunoImplRepositorio
    {
        public AlunoImplRepositorio(ApplicationDbContext contexto) : base(contexto)
        {

        }
    }
}
