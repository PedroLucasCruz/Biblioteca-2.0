using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Repositories;
using Biblioteca._2._0.Infra.Data.Context;
using Biblioteca._2._0.Infra.Data.Repositorys.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca._2._0.Infra.Data.Repositorys
{
    internal class EditoraRepositorio : EFRepositorioGenerico<Editora>, IEditoraRepositorio
    {
        public EditoraRepositorio(ApplicationDbContext contexto) : base(contexto)
        {

        }
    }
}
