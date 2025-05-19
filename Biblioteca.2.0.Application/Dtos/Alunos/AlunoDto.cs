using Biblioteca._2._0.Application.Util;
using Biblioteca._2._0.Application.Validacoes;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Extension.BaseValidacoes;


namespace Biblioteca._2._0.Application.Dtos.Alunos
{
    [Serializable]
    public class AlunoDto : EntidadeBase
    {
        private Conversor<AlunoDto, Aluno> _Conversor;

        private AlunoValidador _Validador;

        //private InconsistenciaDeValidacao _InconsistenciaDeValidacao;

        public InconsistenciaDeValidacaoTipado<Aluno> _InconsistenciaDeValidacao;
        public AlunoDto()
        {
            _Conversor = new Conversor<AlunoDto, Aluno>();
            _Validador = new AlunoValidador();
        }


        public string Nome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }


        public bool EhValidoAtualizacao()
        {
            _InconsistenciaDeValidacao = _Validador.ValideAtualizacaoDeAluno(ObterEntidade());
            return _InconsistenciaDeValidacao.EhValido();
        }

        public bool EhValidoCadastro()
        {
            _InconsistenciaDeValidacao = _Validador.ValideCadastroDeAluno(ObterEntidade());
       
            return _InconsistenciaDeValidacao.EhValido();
        }

        public InconsistenciaDeValidacaoTipado<Aluno> RetornarInconsistencia() => _InconsistenciaDeValidacao;

        public Aluno ObterEntidade() => _Conversor.ConvertaPara(this);

        public AlunoDto ObterDto(Aluno aluno) => _Conversor.ConvertaParaTipo<AlunoDto>(aluno);
        public bool IsValid()
        {
            _InconsistenciaDeValidacao = new AlunoValidador().ValideAtualizacaoDeAluno(this.ObterEntidade());
            return _InconsistenciaDeValidacao.EhValido();
        }
    }
}
