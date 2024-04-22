using Biblioteca._2._0.Extension.Enumerable.TipoValidacaoEnum;

namespace Biblioteca._2._0.Extension.BaseValidacoes
{
    public class InconsistenciaDeValidacao
    {
        public TipoValidacaoEnum TipoValidacao { get; set; }
        public string? PropriedadeValidada { get; set; }
        public string? Mensagem { get; set; }
        public IList<InconsistenciaDeValidacao> ListaDeInconsistencias { get; set; } = new List<InconsistenciaDeValidacao>();
        public IList<string> Mensagens { get; set; } = new List<string>();

        public bool EhValido()
        {
            return ListaDeInconsistencias.Count == 0;
        }      
    }
}
