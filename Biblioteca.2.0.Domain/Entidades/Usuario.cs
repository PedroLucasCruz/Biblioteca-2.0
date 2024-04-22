using Biblioteca._2._0.Application.Util;
using Biblioteca._2._0.Domain.Enumeradores.Usuarios;


namespace Biblioteca._2._0.Domain.Entidades
{
    public class Usuario : EntidadeBase
    {

        public Guid Codigo { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public UsuarioPermissaoEnum Permissao { get; set; }

    }
}
