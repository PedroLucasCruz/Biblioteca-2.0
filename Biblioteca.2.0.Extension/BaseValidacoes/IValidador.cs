
namespace Biblioteca._2._0.Extension.BaseValidacoes
{
    public interface IValidador<T>
    {

        /// <summary>
        /// Verifica se é válido.
        /// </summary>
        /// <param name="t">O nome do parâmetro.</param>
        /// <returns>Retorna um booleano.</returns>
        bool Valido(T t);

        /// <summary>
        /// Método que valida inconsistências de validação.
        /// </summary>
        /// <param name="t">O nome do parâmetro.</param>
        /// <returns>Valida inconsistências de validação.</returns>
        InconsistenciaDeValidacao Valide(T t);

        IList<InconsistenciaDeValidacao> ValideLista(T t);


        InconsistenciaDeValidacaoTipado<T> ValideTipado(T t);

    }
}
