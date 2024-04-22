
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Domain.Interfaces.Repositories;
using Biblioteca._2._0.Extension.BaseValidacoes;
using Biblioteca._2._0.Extension.Enumerable;
using FluentValidation;



namespace Biblioteca._2._0.Domain.ValidacoesNegocio.Livros
{
    public class ServicoLivroValidador : ValidadorAbstratro<Livro>
    {

        private ILivroRepositorio _livroRepositorio;
        private IAutorRepositorio _autorRepositorio;
        private IEditoraRepositorio _editoraRepositorio;

   

        public ServicoLivroValidador()
        {

        }

        public ServicoLivroValidador(ILivroRepositorio livroRepositorio, IAutorRepositorio autorRepositorio, IEditoraRepositorio editoraRepositorio)
        {
            _livroRepositorio = livroRepositorio;
            _autorRepositorio = autorRepositorio;
            _editoraRepositorio = editoraRepositorio;
        }

        public InconsistenciaDeValidacaoTipado<Livro> ValideInicial(Livro dto)
        {
            AssineRegrasItensObrigatorios();

            return base.ValideTipado(dto);
        }





        private void AssineRegrasItensObrigatorios()
        {
            RuleFor(x => x)
                .Must(x => VerifiqueSeEditoraInformadoExiste(x.EditoraId))
                .When(x => x.EditoraId.PossuiValor())
                .SobrescrevaPropriedade("EditoraId")
                .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                .WithMessage("A Editora informado não consta cadastrado no sistema.");

            RuleForEach(x => x.Autores).Cascade(CascadeMode.Continue).ChildRules(v =>
            {

                v.RuleFor(x => x)
                 .Must(x => VerifiqueSeAutoresInformadosExistem(x.AutorId))
                 .When(x => x.AutorId.PossuiValor())
                 .SobrescrevaPropriedade("Autor")
                 .TipoValidacao(TipoValidacaoEnum.IMPEDITIVA)
                 .WithMessage("O Autor informada não consta cadastrada no sistema.");

            });

        }

        private bool VerifiqueSeEditoraInformadoExiste(int IdDaEditora)
        {
           return _editoraRepositorio.ObtenhaPorId(IdDaEditora).EhValido(this);
        
        } 

        private bool VerifiqueSeAutoresInformadosExistem(int IdDoAutor)
        {
            return _autorRepositorio.ObtenhaPorId(IdDoAutor).EhValido(this);

        }

    }
}
