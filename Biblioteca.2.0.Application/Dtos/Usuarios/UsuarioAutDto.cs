
using Biblioteca._2._0.Application.Util;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Extension.BaseValidacoes;
using Biblioteca.Negocio.Validacoes.Usuarios;
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
        private InconsistenciaDeValidacaoTipado<Usuario> _InconsistenciaDeValidacao;
        private AutenticacaoValidador _Validador;

        public UsuarioAutDto()
        {
            _Conversor = new Conversor<UsuarioAutDto, Usuario>();
        }

        public string Nome { get; set; }

        public string Senha { get; set; }

     
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
