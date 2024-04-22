using Biblioteca._2._0.Application.Dtos.Usuarios;
using Biblioteca._2._0.Domain.Entidades;
using Biblioteca._2._0.Extension.BaseValidacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca._2._0.Application.IntefacesAppService
{
    public interface IUsuarioAppService
    {
        Usuario Cadastrar(UsuarioDto obj);

        InconsistenciaDeValidacaoTipado<IList<Usuario>> ObtenhaTodosUsuarios();

        Usuario AtualizeUsuario(UsuarioDto obj);

        InconsistenciaDeValidacao DeleteUsuario(int Id);

        bool AutentiqueUsuario(UsuarioDto obj);

        InconsistenciaDeValidacaoTipado<Usuario> ObterUsuarioId(int id);
    }
}

