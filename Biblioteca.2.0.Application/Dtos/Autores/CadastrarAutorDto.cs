
using Biblioteca._2._0.Application.Util;
using Biblioteca._2._0.Application.Validacoes;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Extension.BaseValidacoes;



namespace Biblioteca._2._0.Application.Dtos.Autores
{
    public class CadastrarAutorDto
    {
        private Conversor<CadastrarAutorDto, Autor> _Conversor;

        private InconsistenciaDeValidacao? inconsistenciaDeValidacao;


        public CadastrarAutorDto()
        {
            _Conversor = new Conversor<CadastrarAutorDto, Autor>();
        }

        public Guid Codigo { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public bool IsValid()
        {
            inconsistenciaDeValidacao = new CadastrarAutoresValidacao().ValidarCadastro(this);
            return inconsistenciaDeValidacao.EhValido();
        }
        public InconsistenciaDeValidacao RetornarInconsistencia() => inconsistenciaDeValidacao;
            
        public Autor ObterEntidade()
        {
            var retorno = _Conversor.ConvertaPara(this);

            retorno.DataCriacao = DateTime.Now;

            return retorno;
        }

    }
}
