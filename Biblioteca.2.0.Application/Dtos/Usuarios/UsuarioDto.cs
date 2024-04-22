using Biblioteca._2._0.Application.Util;
using Biblioteca._2._0.Application.Validacoes;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Enumeradores.Usuarios;
using Biblioteca._2._0.Extension.BaseValidacoes;
using Biblioteca.Negocio.Validacoes.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca._2._0.Application.Dtos.Usuarios
{

    public class UsuarioDto : AutenticacaoValidador
    {
        private Conversor<UsuarioDto, Usuario> _Conversor;
   
        public UsuarioDto()
        {
            _Conversor = new Conversor<UsuarioDto, Usuario>();
        }

        public int Id { get; set; }

        public Guid Codigo { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public UsuarioPermissaoEnum Permissao { get; set; }

        public Usuario ObterEntidade() => _Conversor.ConvertaPara(this);

    }
}
