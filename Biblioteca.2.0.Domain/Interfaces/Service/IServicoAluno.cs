using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Extension.BaseValidacoes;

namespace Biblioteca._2._0.Domain.Interfaces.Service
{
    public interface IServicoAluno
    {
        IList<Aluno> ObtenhaTodosAlunos();

        InconsistenciaDeValidacaoTipado<Aluno> ObtenhaAluno(int Id);

        InconsistenciaDeValidacaoTipado<Aluno> CadastreAluno(Aluno dto);

        InconsistenciaDeValidacaoTipado<Aluno> AtualizeAluno(Aluno dto);

        List<string> DeleteAluno(int Id);
    }
}
