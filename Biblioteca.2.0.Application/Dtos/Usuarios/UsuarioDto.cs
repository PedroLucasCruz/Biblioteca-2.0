using Biblioteca._2._0.Application.Util;
using Biblioteca._2._0.Application.Validacoes;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Enumeradores.Usuarios;
using Biblioteca._2._0.Extension.BaseValidacoes;
using Biblioteca.Negocio.Validacoes.Usuarios;


namespace Biblioteca._2._0.Application.Dtos.Usuarios
{

    public class UsuarioDto : AutenticacaoValidador
    {
        private Conversor<UsuarioDto, Usuario> _Conversor;
        private InconsistenciaDeValidacaoTipado<Usuario> _InconsistenciaDeValidacao;
        private AutenticacaoValidador _Validador;

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


        public bool EhValidoAutenticacao()
        {
            _InconsistenciaDeValidacao = _Validador.ValideAutenticacao(ObterEntidade());
            return _InconsistenciaDeValidacao.EhValido();
        }

        public bool EhValidoCadastro()
        {
            _InconsistenciaDeValidacao = _Validador.ValideCadastro(ObterEntidade());

            return _InconsistenciaDeValidacao.EhValido();
        }

        public InconsistenciaDeValidacaoTipado<Usuario> RetornarInconsistencia() => _InconsistenciaDeValidacao;

        public bool IsValid()
        {
            _InconsistenciaDeValidacao = new AutenticacaoValidador().ValideAutenticacao(ObterEntidade());
            return _InconsistenciaDeValidacao.EhValido();
        }

    }
}
