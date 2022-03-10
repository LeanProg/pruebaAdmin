using RecuperacionT2.Models.Entidades;
using System.Collections.Generic;

namespace RecuperacionT2.Models.Repositorio.RepositorioUsuario
{
    public interface IRepositorioUsuarioSQLite
    {
        void InsertUsuarios(Usuario usuario);
        bool IsUserRegister(string user, string pass);
        Usuario LoginUser(string usuarioNombre);
        List<Usuario> ReadUsuarios();
        void UpdateUsuarios(Usuario unusuario);
        void DeleteUsuario(int id);
        
    }
}