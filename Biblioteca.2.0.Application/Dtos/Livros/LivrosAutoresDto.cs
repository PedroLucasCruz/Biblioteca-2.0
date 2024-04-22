using Biblioteca._2._0.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Biblioteca._2._0.Application.Dtos.Livros
{
    public class LivrosAutoresDto : EntidadeBase
    {
        public int AutorId { get; set; }

    }
}
