using RecuperacionT2.Models.Repositorio;
using RecuperacionT2.Models.Repositorio.RepositorioUsuario;

namespace RecuperacionT2.Models.Repositorio
{
    public class Datadb
    {
        public IRepositorioUsuarioSQLite usuarios { get; set; }

        public Datadb(IRepositorioUsuarioSQLite usuarios)
        {
            this.usuarios = usuarios;
        }
    }
}
