

namespace Biblioteca._2._0.Domain.Core.Messages
{
    public abstract class BaseLogAuditoriaCommand : Command
    {
        public string Funcionalidade { get; protected set; }

        public int IdPerfil { get; protected set; }

        public string Documento { get; protected set; }

        public string Estabelecimento { get; protected set; }

        public string Erro { get; protected set; }
    }
}
