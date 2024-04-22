using Biblioteca._2._0.Application.Util;
using Biblioteca._2._0.Application.Validacoes;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Extension.BaseValidacoes;


namespace Biblioteca._2._0.Application.Dtos.Autores
{
    public class AlterarAutorDto
    {
        private Conversor<AlterarAutorDto, Autor> _Conversor;

        private InconsistenciaDeValidacao? inconsistenciaDeValidacao;

        public AlterarAutorDto()
        {
            _Conversor = new Conversor<AlterarAutorDto, Autor>();
        }

        public int Id { get; set; }
        public Guid Codigo { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }

        public bool IsValid()
        {
            inconsistenciaDeValidacao = new AlterarAutoresValidacao().ValidarAlteracao(this);
            return inconsistenciaDeValidacao.EhValido();
        }

        public InconsistenciaDeValidacao RetornarInconsistencia() => inconsistenciaDeValidacao;

        public Autor ObterEntidade()
        {
            var retorno = _Conversor.ConvertaPara(this);

           retorno.DataAtualizacao = DateTime.Now;

            return retorno;
        }
    }
}
