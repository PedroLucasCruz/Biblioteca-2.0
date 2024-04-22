using Biblioteca._2._0.Application.Dtos.Editoras;
using Biblioteca._2._0.Extension.BaseValidacoes;
using Biblioteca._2._0.Extension.Enumerable.TipoValidacaoEnum;
using FluentValidation;


namespace Biblioteca._2._0.Application.Validacoes
{
    public class CadastroEditoraValidacao : ValidadorAbstratro<CadastroEditoraDto>
    {

        public InconsistenciaDeValidacao ValidarCadastro(CadastroEditoraDto dados)
        {
            AssineRegrasCadastro();
            return base.Valide(dados);
        }

        private void AssineRegrasCadastro()
        {

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
