﻿using Biblioteca._2._0.Application.Util;
using Biblioteca._2._0.Application.Validacoes;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Extension.BaseValidacoes;


namespace Biblioteca._2._0.Application.Dtos.Editoras
{
    public class CadastroEditoraDto
    {

        private Conversor<CadastroEditoraDto, Editora> _Conversor;

        private InconsistenciaDeValidacao? inconsistenciaDeValidacao;

        public CadastroEditoraDto()
        {
            _Conversor = new Conversor<CadastroEditoraDto, Editora>();
        }

        public string Cnpj { get; set; }

        public string Nome { get; set; }

        public string Cidade { get; set; }

        public bool IsValid()
        {
            inconsistenciaDeValidacao = new CadastroEditoraValidacao().ValidarCadastro(this);
            return inconsistenciaDeValidacao.EhValido();
        }
        public InconsistenciaDeValidacao RetornarInconsistencia() => inconsistenciaDeValidacao;

        public Editora ObterEntidade()
        {
            var retorno = _Conversor.ConvertaPara(this);

            retorno.DataCriacao = DateTime.Now;

            return retorno;
        }

    }
}