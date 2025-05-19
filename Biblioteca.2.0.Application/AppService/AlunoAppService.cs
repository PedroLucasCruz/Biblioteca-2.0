using Biblioteca._2._0.Application.Dtos.Alunos;
using Biblioteca._2._0.Application.IntefacesAppService;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Service;
using Biblioteca._2._0.Extension.BaseValidacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca._2._0.Application.ServiceAppService
{

    public class AlunoAppService : IAlunoAppService
    {
        private readonly IServicoAluno _servicoAluno;

        public AlunoAppService(IServicoAluno servicoAluno)
        {
            _servicoAluno = servicoAluno;
        }

        public InconsistenciaDeValidacaoTipado<Aluno> AtualizeAluno(AlunoDto dto)
        {
            if(!dto.IsValid()) return dto.RetornarInconsistencia();

            var resposta = _servicoAluno.AtualizeAluno(dto.ObterEntidade());
            return resposta;
        }

        public InconsistenciaDeValidacaoTipado<Aluno> CadastreAluno(AlunoDto dto)
        {
            if (!dto.IsValid()) return dto.RetornarInconsistencia();

            var retorno = _servicoAluno.CadastreAluno(dto.ObterEntidade());
            return retorno;
        }

        public List<string> DeleteAluno(int Id)
        {
            return _servicoAluno.DeleteAluno(Id);
        }

        public InconsistenciaDeValidacaoTipado<Aluno> ObtenhaAluno(int Id)
        {
            return _servicoAluno.ObtenhaAluno(Id);            
        }

        public IList<Aluno> ObtenhaTodosAlunos()
        {
            return _servicoAluno.ObtenhaTodosAlunos();
        }
    }
}
