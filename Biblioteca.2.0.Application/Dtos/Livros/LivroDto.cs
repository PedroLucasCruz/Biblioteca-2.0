using Biblioteca._2._0.Application.Util;
using Biblioteca._2._0.Application.Validacoes;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Enumeradores.Livros;
using Biblioteca._2._0.Extension.BaseValidacoes;
using Biblioteca._2._0.Extension.Utils;
using System.Text.Json.Serialization;

namespace Biblioteca._2._0.Application.Dtos.Livros
{
    public class LivroDto : EntidadeBase
    {
        private Conversor<LivroDto, Livro> _Conversor;

        private InconsistenciaDeValidacaoTipado<Livro> _Inconsistencias;

        public LivroDto()
        {
            _Conversor = new Conversor<LivroDto, Livro>();
            Autores = new List<LivroAutores>();
        }

        public LivrosTipoOperacaoDeDadosEnum TipoOperacaoDeDadosEnum { private get; set; }

        public Guid Codigo { get; set; }

        public string Titulo { get; set; }

        public string SubTitulo { get; set; }

        public decimal QuantidadeEstoque { get; set; }

        public StatusLivroEnum Status { get; set; }

        [JsonIgnore]
        public IList<LivroAutores> Autores { get; set; }

        public int EditoraId { get; set; }

        public IList<LivrosAutoresDto> AutoresDto { get; set; }


        public bool IsValid()
        {

            switch (TipoOperacaoDeDadosEnum)
            {
                case LivrosTipoOperacaoDeDadosEnum.ALTERAR:
                    _Inconsistencias = new LivrosValidador().ValideAtualizacao(ObterEntidade());
                    break;
                case LivrosTipoOperacaoDeDadosEnum.CADASTRAR:
                    _Inconsistencias = new LivrosValidador().ValideCadastro(ObterEntidade());
                    break;
                case LivrosTipoOperacaoDeDadosEnum.DELETAR:
                    break;
                default:
                    break;
            }

            return _Inconsistencias.EhValido();
        }

        public InconsistenciaDeValidacaoTipado<Livro> RetornarInconsistencia() => _Inconsistencias;

 
        public Livro ObterEntidade()
        {
            var retorno = _Conversor.ConvertaPara(this);

            retorno.DataAtualizacao = DateTime.Now;

            return retorno;
        }

        private void CarregueListaDeAutoresPeloDto()
        {
            if (AutoresDto.PossuiValor() && AutoresDto.PossuiLinhas())
            {

                foreach (var item in AutoresDto)
                {
                    Autores.Add(
                        new LivroAutores()
                        {
                            Codigo = Guid.NewGuid(),
                            AutorId = item.AutorId,
                        }
                        );
                }
            }
        }

    }
}
