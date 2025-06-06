﻿//using FluentValidation;
//using Biblioteca._2._0.Domain.Entidades;
//using Biblioteca._2._0.Domain.Enumeradores.FichaEmprestimoAlunos;
//using Biblioteca._2._0.Domain.Interfaces.Repositories.Base;
//using Biblioteca._2._0.Extension.BaseValidacoes;
//using Biblioteca._2._0.Extension.Utils;
//using Biblioteca._2._0.Extension.Enumerable.TipoValidacaoEnum;
//using FluentValidation;

//namespace Biblioteca._2._0.Domain.ValidacoesNegocio.EmprestimoAlunos
//{
//    public class ServicoValidacaoFichaEmprestimoAluno 
//    {

       


//        public ServicoValidacaoFichaEmprestimoAluno()
//        {

//        }


//        public InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno> ValideFichaCadastro(FichaEmprestimoAluno dados)
//        {
//           // AssineRegrasIniciaisCadastro(dados);
//            AssineRegraDeQuantidadeDeLivrosDisponiveis(dados);
//            AssineRegraDeEmprestimoEmAndamento(dados);

//            return base.ValideTipado(dados);
//        }

//        //public override InconsistenciaDeValidacaoTipado<FichaEmprestimoAluno> ValideFinalizacaoFicha(FichaEmprestimoAluno dados)
//        //{
//        //    AssineRegrasIniciaisFinalizacao(dados);

//        //    return base.ValideTipado(dados);
//        //}



//        #region CADASTRO DA FICHA

//        private void AssineRegraDeQuantidadeDeLivrosDisponiveis(FichaEmprestimoAluno dados)
//        {

//            if (dados.FichaEmprestimoItens.PossuiValor() && dados.FichaEmprestimoItens.PossuiLinhas())
//            {
           
//                RuleForEach(x => x.FichaEmprestimoItens).Cascade(CascadeMode.Continue).ChildRules(v =>
//                {

//                    v.RuleFor(x => x)
//                     .Must(x => LivroPossuiQuantidadePositiva(x.LivroId, x.Quantidade))
//                     .When(x => x.Quantidade > 0)
//                     .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
//                     .SobrescrevaPropriedade("Saldo do Livro")
//                     .WithMessage("O Livro está sem saldo para emprestimo.");



//                });

//            }

//        }

//        private void AssineRegraDeEmprestimoEmAndamento(FichaEmprestimoAluno dados)
//        {
//            RuleFor(x => x)
//                .Must(x => VerifiqueEmprestimoEmAndamentoDoAluno(x.AlunoId))
//                .When(x => x.AlunoId.PossuiValor())
//                .SobrescrevaPropriedade("EmprestimoEmAndamento")
//                .TipoValidacao(Negocio.Enumeradores.Validacoes.TipoValidacaoEnum.IMPEDITIVA)
//                .WithMessage("Não é possível emprestar enquanto não finalizar os Emprestimos em aberto do Aluno.");
//        }

//        private bool LivroPossuiQuantidadePositiva(int LivroId, decimal quantidadeSolicitada)
//        {
//            bool possui = false;
//            decimal quantidadeTotalLivro = 0;
//            decimal quantidadeJaEmprestada = 0;



//            using (IRepositorioGenerico<Livro> repLivro = new EFRepositorioGenerico<Livro>(Contexto))
//            {
//                var livros = repLivro.ObtenhaDbSet().AsNoTracking().ToList();

//                quantidadeTotalLivro = (livros.PossuiValor() && livros.Any(x => x.Id == LivroId))
//                    ? livros.Where(x => x.Id == LivroId).First().QuantidadeEstoque : 0;
//            }


//            using (IRepositorioGenerico<FichaEmprestimoAluno> repFicha = new EFRepositorioGenerico<FichaEmprestimoAluno>(Contexto))
//            {
//                var lista = repFicha.ObtenhaDbSet()
//               .AsNoTracking()
//               .Where(x => x.StatusEmprestimo == FichaEmprestimoAlunoStatusEnum.NORMAL && x.FichaEmprestimoItens.Any(x => x.LivroId == LivroId))
//               .Include(x => x.FichaEmprestimoItens)
//               .ToList();

//                if (lista.PossuiValor() && lista.PossuiLinhas())
//                {
//                    foreach (var item in lista)
//                    {
//                        quantidadeJaEmprestada += item.FichaEmprestimoItens.Where(x => x.StatusItem == FichaEmprestimoAlunoItensStatusEnum.A_ENTREGAR).Sum(x => x.Quantidade);
//                    }
//                }
//            }


//            possui = quantidadeTotalLivro > quantidadeJaEmprestada;

//            if (possui)
//            {
//                var saldo = quantidadeTotalLivro - quantidadeJaEmprestada;
//                possui = saldo - quantidadeSolicitada > 0;
//            }

//            return possui;
//        }

//        private bool VerifiqueEmprestimoEmAndamentoDoAluno(int AlunoId)
//        {

//            using (IRepositorioGenerico<FichaEmprestimoAluno> repFicha = new EFRepositorioGenerico<FichaEmprestimoAluno>(Contexto))
//            {
//                return !repFicha.ObtenhaDbSet()
//                .AsNoTracking()
//                .Any(x => x.AlunoId == AlunoId
//                && x.StatusEmprestimo == FichaEmprestimoAlunoStatusEnum.NORMAL
//                || x.StatusEmprestimo == FichaEmprestimoAlunoStatusEnum.ATRASADO);
//            }


//        }


//        #endregion



//    }
//}
