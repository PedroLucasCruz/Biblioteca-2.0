using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Extension.BaseValidacoes;
using Biblioteca._2._0.Extension.Enumerable;
using Biblioteca._2._0.Extension.Enumerable.TipoValidacaoEnum;
using FluentValidation;


namespace Biblioteca._2._0.Application.Validacoes
{
    public class AlunoValidador : ValidadorAbstratro<Aluno>
    {
        public AlunoValidador()
        {

        }

        public InconsistenciaDeValidacaoTipado<Aluno> ValideAtualizacaoDeAluno(Aluno dados)
        {

            AssineRegrasDeCadastro();
            return base.ValideTipado(dados);
        }

        public InconsistenciaDeValidacaoTipado<Aluno> ValideCadastroDeAluno(Aluno dados)
        {


            AssineRegrasDeCadastro();
            return base.ValideTipado(dados);
        }


        private void AssineRegrasDeCadastro()
        {

            RuleFor(x => x.Nome)
                .NotNull()
                .NotEmpty()
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .WithMessage("Deve ser informado um Nome");

            RuleFor(x => x.Telefone)
                .NotNull()
                .NotEmpty()
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .WithMessage("Deve ser informado um Telefone");

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .WithMessage("Deve ser informado um email");
        }

    }
}
