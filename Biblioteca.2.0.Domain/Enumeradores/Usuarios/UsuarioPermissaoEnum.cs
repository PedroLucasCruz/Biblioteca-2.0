﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca._2._0.Domain.Enumeradores.Usuarios
{
    public enum UsuarioPermissaoEnum
    {
        ADMINISTRADOR = 1,

        OPERADOR = 2


    }

    public static class Permissoes
    {
        public const string ADMINISTRADOR = "ADMINISTRADOR";
        public const string OPERADOR = "OPERADOR";
    }
}
