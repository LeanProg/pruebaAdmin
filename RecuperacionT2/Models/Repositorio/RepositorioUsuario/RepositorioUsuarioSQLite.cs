using System;
using System.Collections.Generic;
using System.Data.SQLite;
using RecuperacionT2.Models.Entidades;
namespace RecuperacionT2.Models.Repositorio.RepositorioUsuario
{
   
        public class RepositorioUsuarioSQLite : IRepositorioUsuarioSQLite
        {

            private readonly SQLiteConnection conexion;
            private string connectionString;

            public RepositorioUsuarioSQLite(string connectionString)
            {

                this.connectionString = connectionString;
            }
            //leo todos los usuarios
            public List<Usuario> ReadUsuarios()
            {
                List<Usuario> misUsuarios = new List<Usuario>();
                try
                {
                    string Query = "SELECT * FROM Usuarios WHERE Activo=1;";
                    using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                    {
                         conexion.Open();
                         SQLiteCommand command = new SQLiteCommand(Query, conexion);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Usuario usuario = new Usuario();
                                    usuario.Idusuario=Convert.ToInt32(reader["usuarioID"]);
                                    usuario.Nombreusuario = Convert.ToString(reader["usuarioNombre"]);
                                    usuario.Clave = Convert.ToString(reader["usuarioClave"]);
                                    misUsuarios.Add(usuario);
                                }
                                reader.Close();

                            }
                        }
                         conexion.Close();
                }
                    
                }
                catch (Exception ex)
                {

                    string error = ex.ToString();
                }
                return misUsuarios;
            }
            //inserto un usuario en la base de datos
            public void InsertUsuarios(Usuario usuario)
            {
                string query = "INSERT INTO Usuarios (usuarioNombre,usuarioClave,Activo) VALUES (@usu,@pass,1)";
                try
                {
                    using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                    {
                        using (SQLiteCommand command = new SQLiteCommand(query, conexion))
                        {
                            conexion.Open();
                            command.Parameters.AddWithValue("@usu", usuario.Nombreusuario);
                            command.Parameters.AddWithValue("@pass", usuario.Clave);
                            command.ExecuteNonQuery();
                            conexion.Close();
                            string info = "El usuario " + usuario.Nombreusuario + " fue creado satisfactoriamente";

                        }
                    }

                }
                catch (Exception ex)
                {

                    string error = "El usuario no pudo crearse" + ex.ToString();

                }



            }
            //veo si el usuario existe o no
            public bool IsUserRegister(string user, string pass)
            {
                bool resultado = false;
                Usuario logeoUsuaro = new Usuario();
                string query = "SELECT * FROM Usuarios WHERE usuarioNombre=@usa AND usuarioClave=@clave;";
                try
                {
                    using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                    {
                        conexion.Open();
                        using (SQLiteCommand command = new SQLiteCommand(query, conexion))
                        {
                            command.Parameters.AddWithValue("@usa", user);
                            command.Parameters.AddWithValue("@clave", pass);
                            command.ExecuteNonQuery();
                            using (SQLiteDataReader dataReader = command.ExecuteReader())
                            {
                                if (dataReader.HasRows)
                                {
                                    while (dataReader.Read())
                                    {
                                        logeoUsuaro.Nombreusuario = Convert.ToString(dataReader["usuarioNombre"]);
                                        logeoUsuaro.Clave = Convert.ToString(dataReader["usuarioClave"]);
                                        resultado = true;
                                    }
                                    dataReader.Close();
                                    conexion.Close();
                                }
                            }
                        }

                    }
                }
                catch (Exception ex)
                {

                    string error = ex.ToString();
                }
                return resultado;
            }
            public Usuario LoginUser(string usuarioNombre)
            {

                Usuario usuariologueado = new Usuario();
                string query = "SELECT * FROM  Usuarios Where usuarioNombre=@user;";
                try
                {
                    using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                    {
                        SQLiteCommand command = new SQLiteCommand(query, conexion);
                        conexion.Open();
                        command.Parameters.AddWithValue("@user", usuarioNombre);
                        command.ExecuteNonQuery();
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {
                                    usuariologueado.Nombreusuario = Convert.ToString(reader["usuarioNombre"]);
                                    usuariologueado.Clave = Convert.ToString(reader["usuarioClave"]);
                                }
                                reader.Close();

                            }
                        }
                        conexion.Close();
                    }
                }
                catch (Exception ex)
                {

                    string message = ex.ToString();
                }

                return usuariologueado;
            }

                public void UpdateUsuarios(Usuario unusuario)
                {
                            try
                            {
                                string instruccion = @"UPDATE Usuarios
                                                       SET  usuarioNombre = @Nombre, usuarioClave = @Contrasenia WHERE usuarioID = @IdUsuario;";

                                using (var conexion = new SQLiteConnection(connectionString))
                                {
                                    using (SQLiteCommand command = new SQLiteCommand(instruccion, conexion))
                                    {
                                        command.Parameters.AddWithValue("@IdUsuario", unusuario.Idusuario);
                                        command.Parameters.AddWithValue("@Nombre", unusuario.Nombreusuario);
                                        command.Parameters.AddWithValue("@Contrasenia", unusuario.Clave);
                                        
                                        conexion.Open();
                                        command.ExecuteNonQuery();
                                        conexion.Close();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                string error = ex.ToString();
                                
                            }
                }

                        public void DeleteUsuario(int id)
                        {
                                try
                                {
                                    string instruccion = @"UPDATE Usuarios
                                                           SET  Activo=@Activo
                                                           WHERE usuarioID= @IdUsuario";

                                    using (var conexion = new SQLiteConnection(connectionString))
                                    {
                                        using (SQLiteCommand command = new SQLiteCommand(instruccion, conexion))
                                        {
                                            command.Parameters.AddWithValue("@Activo", 0);
                                            command.Parameters.AddWithValue("@IdUsuario", id);
                                            conexion.Open();
                                            command.ExecuteNonQuery();
                                            conexion.Close();
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    string error = ex.ToString();
                                    
                                }
                         }
        
        }

    
}
