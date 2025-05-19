using Biblioteca._2._0.Application.Util;
using Biblioteca._2._0.Application.Validacoes;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Enumeradores.FichaEmprestimoAlunos;
using Biblioteca._2._0.Extension.BaseValidacoes;
using Biblioteca._2._0.Extension.Utils;
using System.Text.Json.Serialization;


namespace Biblioteca._2._0.Application.Dtos.FichaEmprestimoAlunos
{
    public class FichaEmprestimoAlunoDto 
    {

        private Conversor<FichaEmprestimoAlunoDto, FichaEmprestimoAluno> _Conversor;

        public InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno> inconsistenciaDeValidacao;

        public FichaEmprestimoAlunoDto()
        {
            _Conversor = new Conversor<FichaEmprestimoAlunoDto, FichaEmprestimoAluno>();
            FichaEmprestimoItens = new List<FichaEmprestimoItem>();
        }

        public Guid Codigo { get; set; }

        public int UsuarioId { get; set; }

        public int AlunoId { get; set; }

        public string Observacoes { get; set; }

        public string StatusEmprestimoDescricao { get; set; }

        [JsonIgnore]
        public FichaEmprestimoAlunoStatusEnum StatusEmprestimo { get; set; }

        public IList<FichaEmprestimoAlunoItensDto> FichaEmprestimoItensDto { get; set; }

        [JsonIgnore]
        public IList<FichaEmprestimoItem> FichaEmprestimoItens { get; set; }

        public DateTime DataEmprestimo { get; set; }

        public DateTime DataVencimentoEmprestimo { get; set; }

        public DateTime DataEntregaEmprestimo { get; set; }


        public bool IsValid()
        {
            inconsistenciaDeValidacao = new FichaEmprestimoAlunoValidador().ValideCadastroFicha(this); 
            return inconsistenciaDeValidacao.EhValido();
        }

        public InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno> RetornarInconsistencia() => inconsistenciaDeValidacao;

        public FichaEmprestimoAluno ObterEntidade()
        {
            var retorno = _Conversor.ConvertaPara(this);

            retorno.DataCriacao = DateTime.Now;

            return retorno;
        }

        //public bool ValideCadastroFicha()
        //{

        //    _InconsistenciaDeValidacoes = new FichaEmprestimoAlunoValidador().ValideCadastroFicha(this); //Passa a propria classe Dto por parametro
        //    return _InconsistenciaDeValidacoes.EhValido();
        //}

        //public bool ValideFinalizacaoFicha()
        //{
        //    _InconsistenciaDeValidacoes = new FichaEmprestimoAlunoValidador().ValideFinalizacaoFicha(this);
        //    return _InconsistenciaDeValidacoes.EhValido();
        //}

        //public InconsistenciaDeValidacaoTipado<FichaEmprestimoAlunoDto> RetornarInconsistencias() => _InconsistenciaDeValidacoes;

        //private void PreenchaStatusDoEmprestimoPelaDescricaoDoDto()
        //{
        //    if (StatusEmprestimoDescricao.PossuiValor())
        //    {
        //        foreach (FichaEmprestimoAlunoStatusEnum item in Enum.GetValues(typeof(FichaEmprestimoAlunoStatusEnum)))
        //        {
        //            if (item.ObtenhaDescricao().Equals(StatusEmprestimoDescricao))
        //            {
        //                StatusEmprestimo = item;
        //            }
        //        }
        //    }
        //}

        //private void PreenchaListaDeItensPeloDto()
        //{
        //    if (FichaEmprestimoItensDto.PossuiValor() && FichaEmprestimoItensDto.PossuiLinhas())
        //    {
        //        foreach (var item in FichaEmprestimoItensDto)
        //        {
        //            FichaEmprestimoItens.Add(
        //                new FichaEmprestimoItem()
        //                {
        //                    Codigo = Guid.NewGuid(),
        //                    LivroId = item.LivroId,
        //                    StatusItem = item.StatusItem,
        //                    DataStatusItem = DateTime.Now,
        //                    Quantidade = item.Quantidade,
        //                }
        //                );
        //        }
        //    }
        //}



        //public FichaEmprestimoAluno ObtenhaEntidade()
        //{
        //    PreenchaStatusDoEmprestimoPelaDescricaoDoDto();
        //    PreenchaListaDeItensPeloDto();

        //    return _Conversor.ConvertaPara(this);
        //}

    }
}
