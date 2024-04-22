using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Extension.BaseValidacoes;
using Biblioteca._2._0.Extension.Enumerable.TipoValidacaoEnum;
using Biblioteca._2._0.Extension.Utils;
using FluentValidation;


namespace Biblioteca._2._0.Application.Validacoes
{
    public class LivrosValidador : ValidadorAbstratro<Livro>
    {

        public LivrosValidador() { }


        public InconsistenciaDeValidacaoTipado<Livro> ValideCadastro(Livro livro)
        {
            AssineRegrasDeCadastro();
            return base.ValideTipado(livro);
        }


        public InconsistenciaDeValidacaoTipado<Livro> ValideAtualizacao(Livro livro)
        {
            AssineRegrasDeCadastro();
            AssineRegrasDeAtualizacao();
            return base.ValideTipado(livro);
        }



        private void AssineRegrasDeCadastro()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty()
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .WithMessage("Deve incluir um título ao Livro");

            RuleFor(x => x.SubTitulo)
                .NotEmpty()
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .WithMessage("Deve incluir um sub título ao Livro");

            RuleFor(x => x.QuantidadeEstoque)
                .NotEmpty()
                .NotEqual(0)
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .WithMessage("Deve incluir um estoque de ao menos 1 quantidade.");

            RuleFor(x => x)
                .Must(x => x.Autores.PossuiLinhas())
                .When(x => x.Autores.PossuiValor())
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .WithMessage("Deve ser informado pelo menos um autor para o livro");

            RuleFor(x => x)
               .Must(x => x.EditoraId != 0)
               .Must(x => x.EditoraId.PossuiValor())
               .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
               .WithMessage("Deve ser informado um código de Editora válido");

        }

        private void AssineRegrasDeAtualizacao()
        {

            RuleFor(x => x.DataAtualizacao)
                .GreaterThan(DateTime.Now)
                .TipoValidacao(TipoValidacaoEnum.ADVERTENCIA)
                .WithMessage("A Data de atualização deve ser a data de hoje e ser menor que a data de cadastro");

        }
    }
}
