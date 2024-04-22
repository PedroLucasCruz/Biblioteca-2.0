using Biblioteca._2._0.Application.Util;
using Biblioteca._2._0.Application.Validacoes;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Extension.BaseValidacoes;


namespace Biblioteca._2._0.Application.Dtos.Editoras
{
    public class AlterarEditoraDto
    {

        private Conversor<AlterarEditoraDto, Editora> _Conversor;

        private InconsistenciaDeValidacao? inconsistenciaDeValidacao;

        public AlterarEditoraDto()
        {
            _Conversor = new Conversor<AlterarEditoraDto, Editora>();
        }

        public int Id { get; set; }
        public Guid Codigo { get; set; }
        public string Cnpj { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }


        public bool IsValid()
        {
            inconsistenciaDeValidacao = new AlterarEditoraValidacao().ValidarAlteracao(this);
            return inconsistenciaDeValidacao.EhValido();
        }

        public InconsistenciaDeValidacao RetornarInconsistencia() => inconsistenciaDeValidacao;

        public Editora ObterEntidade()
        {
            var retorno = _Conversor.ConvertaPara(this);

            retorno.DataAtualizacao = DateTime.Now;

            return retorno;
        }
    }
}
