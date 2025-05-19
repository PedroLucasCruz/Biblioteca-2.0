using Biblioteca._2._0.Application.Dtos.FichaEmprestimoAlunos;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Enumeradores.FichaEmprestimoAlunos;
using Biblioteca._2._0.Extension.BaseValidacoes;
using Biblioteca._2._0.Extension.Enumerable.TipoValidacaoEnum;
using Biblioteca._2._0.Extension.Utils;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca._2._0.Application.Validacoes
{
    public class FichaEmprestimoAlunoValidador : ValidadorAbstratro<FichaEmprestimoAluno>
    {
        public FichaEmprestimoAlunoValidador()
        {

        }


        public InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno> ValideCadastroFicha(FichaEmprestimoAlunoDto dados)
        {
            AssineRegrasCamposObrigatorios(dados);
            AssineRegrasDeCadastroDaFicha();
            return base.ValideTipado(dados);
        }

        public virtual InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno> ValideFinalizacaoFicha(FichaEmprestimoAlunoDto dados)
        {
            AssineRegrasCamposObrigatorios(dados);
           // AssineRegraDeFinalizacaoDaFicha(dados);

            return base.ValideTipado(dados);
        }

        protected void AssineRegrasIniciaisCadastro(FichaEmprestimoAluno dados)
        {
            AssineRegrasCamposObrigatorios(dados);
            AssineRegrasDeCadastroDaFicha();
        }


        protected void AssineRegrasIniciaisFinalizacao(FichaEmprestimoAluno dados)
        {
            AssineRegrasCamposObrigatorios(dados);
           // AssineRegraDeFinalizacaoDaFicha(dados);
        }

        private void AssineRegrasCamposObrigatorios(FichaEmprestimoAluno dados)
        {

            RuleFor(x => x.AlunoId)
                .NotNull()
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .WithMessage("Deve ser informado o aluno");



            RuleFor(x => x.UsuarioId)
                .NotNull()
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .WithMessage("Deve ser informado o usuário logado no sistema.");



            RuleFor(x => x.FichaEmprestimoItens)
                .NotNull()
                .NotEmpty()
                .SobrescrevaPropriedade("ItensDaFichaDoAluno")
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .WithMessage("Deve ser informado os itens da ficha.");


            RuleFor(x => x.Codigo)
                .NotNull()
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .WithMessage("Deve ser informado um código do tipo Guid");


        }

        private void AssineRegrasDeCadastroDaFicha()
        {

            RuleFor(x => x)
                .Must(x => x.StatusEmprestimo == FichaEmprestimoAlunoStatusEnum.NORMAL)
                .When(x => x.PossuiValor())
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .SobrescrevaPropriedade("Status Ficha")
                .WithMessage("Para cadastro de ficha o status deve ser informado como NORMAL");

            RuleFor(x => x)
                .Must(x => x.FichaEmprestimoItens.Count <= 3)
                .When(x => x.FichaEmprestimoItens.PossuiValor() && x.FichaEmprestimoItens.PossuiLinhas())
                .SobrescrevaPropriedade("Quantidades de Itens")
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .WithMessage("Não é permitido mais que 3 livros por ficha.");
        }


        //private void AssineRegraDeFinalizacaoDaFicha(FichaEmprestimoAluno dados)
        //{

        //    RuleFor(x => x.DataEntregaEmprestimo)
        //        .NotEqual(x => x.DataCriacao)
        //        .TipoValidacao(Enumeradores.Validacoes.TipoValidacaoEnum.IMPEDITIVA)
        //        .SobrescrevaPropriedade("Data de Entrega")
        //        .WithMessage("A data da entrega tem de ser diferente da data de cadastro do livro.");

        //    RuleFor(x => x.DataEntregaEmprestimo)
        //        .GreaterThan(x => x.DataCriacao)
        //        .TipoValidacao(Enumeradores.Validacoes.TipoValidacaoEnum.IMPEDITIVA)
        //        .SobrescrevaPropriedade("Data de Entrega")
        //        .WithMessage("A data da entrega tem de ser maior que a data de cadastro.");


        //    RuleFor(x => x.StatusEmprestimo)
        //        .Equal(Enumeradores.FichaEmprestimoAlunos.FichaEmprestimoAlunoStatusEnum.ENTREGUE)
        //        .TipoValidacao(Enumeradores.Validacoes.TipoValidacaoEnum.IMPEDITIVA)
        //        .WithMessage("Para finalização da ficha deve ser informado o status como ENTREGUE");

        //    RuleForEach(x => x.FichaEmprestimoItens).Cascade(CascadeMode.Continue).ChildRules(v =>
        //    {

        //        v.RuleFor(x => x.StatusItem)
        //        .NotNull()
        //        .Equal(Enumeradores.FichaEmprestimoAlunos.FichaEmprestimoAlunoItensStatusEnum.ENTREGUE)
        //        .TipoValidacao(Enumeradores.Validacoes.TipoValidacaoEnum.IMPEDITIVA)
        //        .WithMessage("Para finalização de ficha deve informar o item como ENTREGUE");

        //    });

        //}
    }
}
