using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Repositories;
using Biblioteca._2._0.Domain.Interfaces.Repositories.Base;
using Biblioteca._2._0.Domain.Interfaces.Service;
using Biblioteca._2._0.Extension.BaseValidacoes;
using Microsoft.Extensions.Logging;


namespace Biblioteca._2._0.Domain.Services
{
    public class ServicoAlunoImpl :  IServicoAluno
    {

        private readonly ILogger<IServicoAluno> _logger;
        private readonly IAlunoImplRepositorio _alunoImplRepositorio;

        public ServicoAlunoImpl(ILogger<IServicoAluno> logger, IAlunoImplRepositorio servicoAlunoImplRepositorio)
        {
            _logger = logger;
            _alunoImplRepositorio = servicoAlunoImplRepositorio;
        }

        public InconsistenciaDeValidacaoTipado<Aluno> AtualizeAluno(Aluno dto)
        {
            _logger.LogInformation("Serviço: Iniciando a atualização do Aluno");

            try
            {

                _alunoImplRepositorio.Altere(ObtenhaAlunoParaAtualizacao(dto));
                _logger.LogInformation("Serviço: Aluno atualizado Corretamente");
                var alunoAtualizado = _alunoImplRepositorio.ObtenhaPorId(dto.Id);
                return new InconsistenciaDeValidacaoTipado<Aluno>() { ObjetoRetorno = alunoAtualizado, Mensagem = "Atualizado com Sucesso" };


            }
            catch (Exception ex)
            {
                _logger.LogError("Serviço: Erro na atualização do Aluno", ex);
                return null;
            }
        }

        public InconsistenciaDeValidacaoTipado<Aluno> CadastreAluno(Aluno dto)
        {
            _logger.LogInformation("Serviço: Iniciando o cadastro do Aluno");
            try
            {
                _logger.LogInformation("Serviço: Ajustando datas do cadastro");
                dto.DataCriacao = DateTime.Now;
                dto.DataAtualizacao = DateTime.Now;         
                _logger.LogInformation("Serviço: Criando código Guid no cadastro");            

                _alunoImplRepositorio.Cadastre(dto);
                _logger.LogInformation("Serviço: Aluno cadastrado corretamente");

               var retorno = _alunoImplRepositorio.ObtenhaPorId(dto.Id);

               return new InconsistenciaDeValidacaoTipado<Aluno>() { ObjetoRetorno = retorno, Mensagem = "Atualizado com Sucesso" };
                //return base._DbSet.Where(x => x.Codigo == dto.Codigo).First();
            }

            catch (Exception ex)
            {
                _logger.LogError("Serviço: Erro no cadastro do Aluno", ex);
                return null;
            }
        }


        public List<string> DeleteAluno(int Id)
        {
            //_logger.LogInformation("Serviço: Iniciando a remoção do Aluno");
            //try
            //{

            //    _logger.LogInformation("Serviço: Iniciando a busca por fichas de emprestimo do Aluno.");
            //    using (IRepositorioGenerico<FichaEmprestimoAluno> ser = new EFRepositorioGenerico<FichaEmprestimoAluno>(_contexto))
            //    {
            //        var resposta = ser.ObtenhaDbSet().AsNoTracking().Where(x => x.AlunoId == Id || x.Aluno.Id == Id).ToList();
            //        if (resposta.PossuiValor() && resposta.PossuiLinhas())
            //        {
            //            List<string> lista = new List<string>();
            //            resposta.ToList().ForEach(x => lista.Add($"Ficha de Emprestimo Código: {x.Id.ToString("00000000")}"));
            //            return lista;
            //        }
            //    }


            //    var res = _servicoAlunoImplRepositorio.Delete(Id);
            //    _logger.LogInformation("Serviço: Remoção do aluno executada");
            //    return null;
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError("Serviço: Erro ao remover aluno.", ex);
            //    return null;
            //}
            return null;

        }

        public InconsistenciaDeValidacaoTipado<Aluno> ObtenhaAluno(int Id)
        {
            _logger.LogInformation("Serviço: Iniciando a busca do aluno");
            try
            {
                var aluno = _alunoImplRepositorio.ObtenhaPorId(Id);
                _logger.LogInformation("Serviço: Aluno encontrado com sucesso.");
                return  new InconsistenciaDeValidacaoTipado<Aluno>() { ObjetoRetorno = aluno, Mensagem = "Atualizado com Sucesso" }; ;
            }
            catch (Exception ex)
            {
                _logger.LogError("Serviço: Erro ao tentar obter aluno", ex);
                return null;

            }
        }

        public IList<Aluno> ObtenhaTodosAlunos()
        {
            _logger.LogInformation("Serviço: Iniciando a busca dos alunos");
            try
            {
                var res = _alunoImplRepositorio.ObtenhaTodos();
                _logger.LogInformation("Serviço: Alunos encontrados");
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError("Serviço: Erro ao tentar obter a lista de alunos.", ex);
                return null;
            }

        }

        private Aluno ObtenhaAlunoParaAtualizacao(Aluno dto)
        {

            ///AsNoTracking() usado para não gerar o Cache no contexto atual.
            ///
            var AlunoAtual = _alunoImplRepositorio.ObtenhaPorId(dto.Id);

            dto.Id = dto.Id;
            //dto.Codigo = AlunoAtual.Codigo;
            dto.Nome = AlunoAtual.Nome != dto.Nome ? dto.Nome : AlunoAtual.Nome;
            dto.Email = AlunoAtual.Email != dto.Email ? dto.Email : AlunoAtual.Email;
            dto.Telefone = AlunoAtual.Telefone != dto.Telefone ? dto.Telefone : AlunoAtual.Telefone;
            dto.DataCriacao = AlunoAtual.DataCriacao;
            dto.DataAtualizacao = DateTime.Now;

            return dto;

        }


    }
}
