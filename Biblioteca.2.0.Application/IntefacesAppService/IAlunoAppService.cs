using Biblioteca._2._0.Application.Dtos.Alunos;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Extension.BaseValidacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca._2._0.Application.IntefacesAppService
{
    public interface IAlunoAppService
    {
        IList<Aluno> ObtenhaTodosAlunos();

        InconsistenciaDeValidacaoTipado<Aluno> ObtenhaAluno(int Id);

        InconsistenciaDeValidacaoTipado<Aluno> CadastreAluno(AlunoDto dto);

        InconsistenciaDeValidacaoTipado<Aluno> AtualizeAluno(AlunoDto dto);

        List<string> DeleteAluno(int Id);
    }
}
