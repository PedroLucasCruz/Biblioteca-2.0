
using Biblioteca._2._0.Application.Util;
using Biblioteca._2._0.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca._2._0.Application.Dtos.Usuarios
{
    public class UsuarioAutDto
    {
        private Conversor<UsuarioAutDto, Usuario> _Conversor;
        public UsuarioAutDto()
        {
            _Conversor = new Conversor<UsuarioAutDto, Usuario>();
        }

        public string Nome { get; set; }

        public string Senha { get; set; }

        public Usuario ObtenhaEntidade() => _Conversor.ConvertaPara(this);
    }
}
