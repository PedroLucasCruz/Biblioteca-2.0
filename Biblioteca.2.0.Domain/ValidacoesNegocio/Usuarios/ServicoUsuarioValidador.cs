using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Repositories;
using Biblioteca._2._0.Extension.BaseValidacoes;
using Biblioteca._2._0.Extension.Enumerable.TipoValidacaoEnum;
using Biblioteca.Negocio.Validacoes.Usuarios;
using FluentValidation;


namespace Biblioteca._2._0.Domain.ValidacoesNegocio.Usuarios
{
    public class ServicoUsuarioValidador : AutenticacaoValidador
    {

    
        private readonly IUsuarioRepositorio _usuarioRepositorio;

    
        public ServicoUsuarioValidador()
        {

        }

        public ServicoUsuarioValidador(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public InconsistenciaDeValidacaoTipado<Usuario> ValideCadastroImpeditivo(Usuario dados)
        {

            ValideDuplicidadeDeCadastro(dados);

            return base.ValideTipado(dados);
        }


        private void ValideDuplicidadeDeCadastro(Usuario dados)
        {

            RuleFor(x => x.Nome)
                .Must(x => ExisteNomeCadastrado(dados.Nome))
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .WithMessage("Nome Já Cadastrado no Sistema");

            RuleFor(x => x.Email)
                .Must(x => ExisteEmailJaCadastrado(dados.Email))
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .WithMessage("Já existe um cadastro com esse E-Mail");
        }


        private bool ExisteEmailJaCadastrado(string email)
        {
            bool existe = false;                     
            var lista = _usuarioRepositorio.ObtenhaTodos().ToList(); 
            existe = lista.Any(x => x.Email == email);
            
            return existe;
        }

        private bool ExisteNomeCadastrado(string nome)
        {
            bool existe = false;

            var lista = _usuarioRepositorio.ObtenhaTodos().ToList();

            existe = lista.Any(x => x.Nome == nome);
            
            return existe;
        }
    }
}
