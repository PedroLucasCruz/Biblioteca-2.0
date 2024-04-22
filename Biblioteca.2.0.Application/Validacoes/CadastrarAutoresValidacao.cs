using Biblioteca._2._0.Application.Dtos.Autores;
using Biblioteca._2._0.Extension.BaseValidacoes;
using Biblioteca._2._0.Extension.Enumerable.TipoValidacaoEnum;
using FluentValidation;


namespace Biblioteca._2._0.Application.Validacoes
{
    public class CadastrarAutoresValidacao  : ValidadorAbstratro<CadastrarAutorDto>
    {
        public InconsistenciaDeValidacao ValidarCadastro(CadastrarAutorDto dados)
        {
            AssineRegrasCadastro();
            return Valide(dados);
        }

        private void AssineRegrasCadastro()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .WithMessage("Deve ser informado um Nome ");


            RuleFor(x => x.Telefone)
                .NotEmpty()
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .WithMessage("Deve ser informado um telefone ");
        }
    }
}
