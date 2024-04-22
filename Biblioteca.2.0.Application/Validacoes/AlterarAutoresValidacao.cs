using Biblioteca._2._0.Application.Dtos.Autores;
using Biblioteca._2._0.Extension.BaseValidacoes;
using Biblioteca._2._0.Extension.Enumerable.TipoValidacaoEnum;

using FluentValidation;


namespace Biblioteca._2._0.Application.Validacoes
{
    public class AlterarAutoresValidacao : ValidadorAbstratro<AlterarAutorDto>
    {
        public InconsistenciaDeValidacao ValidarAlteracao(AlterarAutorDto dados)
        {
            AssineRegrasAlteracao();
            return Valide(dados);
        }

        private void AssineRegrasAlteracao()
        {
            var retorno = TipoValidacaoEnum.IMPEDITIVA;

            RuleFor(x => x.Id)
                 .NotEmpty()
                 .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                 .WithMessage("Deve ser informado um Id");

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
