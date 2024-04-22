using Biblioteca._2._0.Application.Dtos.Editoras;
using Biblioteca._2._0.Extension.BaseValidacoes;
using Biblioteca._2._0.Extension.Enumerable;
using Biblioteca._2._0.Extension.Enumerable.TipoValidacaoEnum;
using FluentValidation;


namespace Biblioteca._2._0.Application.Validacoes
{
    public class AlterarEditoraValidacao : ValidadorAbstratro<AlterarEditoraDto>
    {

        public InconsistenciaDeValidacao ValidarAlteracao(AlterarEditoraDto dados)
        {
            AssineRegrasAlteracao();
            return base.Valide(dados);
        }

        private void AssineRegrasAlteracao()
        {

            RuleFor(x => x.Id)
                 .NotEmpty()
                 .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                 .WithMessage("Deve ser informado um Id");

            RuleFor(x => x.Cnpj)
                .NotEmpty()
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .WithMessage("Deve ser informado um Cnpj ");

            RuleFor(x => x.Nome)
                .NotEmpty()
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .WithMessage("Deve ser informado um Nome ");


            RuleFor(x => x.Cidade)
                .NotEmpty()
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .WithMessage("Deve ser informado uma cidade ");
        }
    }
}
