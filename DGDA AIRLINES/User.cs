using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregar los namespaces de conexión con SQL Server
using System.Data.SqlClient;
using System.Configuration;

namespace DGDA_AIRLINES
{
    //Variable que contiene los valores del estado del usuario
    public enum EstadosUsuario
    {
        Active = 'A',
        Inactive = 'I',
    }

    class User
    {
        // Variables miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["DGDA_AIRLINES.Properties.Settings.DGDA_AIRLINES"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        // Propiedades
        public int ID { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public EstadosUsuario Status { get; set; }

        // Constructores
        public User() { }

        public User(string nombreCompleto, string username, string password, EstadosUsuario estado)
        {
            Name = nombreCompleto;
            Username = username;
            Password = password;
            Status = estado;
        }

        // Métodos
        /// <summary>
        /// Obtiene un usuario por su ID
        /// </summary>
        /// <param name="id">El id del usuario</param>
        /// <returns>Los datos del usuario</returns>
        public User BuscarUsuarioID(int id)
        {
            // Crear el objeto que almacena la información de los resultados
            User usuario = new User();

            try
            {
                // Query de selección
                string query = @"SELECT * FROM Usuarios.usuario
                                 WHERE id = @id";


                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@id", ID);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Obtener los valores del usuario si la consulta retorna valores
                        usuario.ID = Convert.ToInt32(rdr["id"]);
                        usuario.Name = rdr["nombreCompleto"].ToString();
                        usuario.Username = rdr["username"].ToString();
                        usuario.Password = rdr["password"].ToString();
                        usuario.Status = (EstadosUsuario)Convert.ToChar(rdr["estado"].ToString().Substring(0, 1));
                    }
                }

                // Retornar el usuario con los valores
                return usuario;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // Cerrar la conexión
                sqlConnection.Close();
            }
        }

        // Métodos
        /// <summary>
        /// Retorna el estado del usuario desde el enum 'EstadosUsuario'
        /// </summary>
        /// <param name="estado">El valor dentro del enum</param>
        /// <returns>Estado válido dentro de la base de datos</returns>
        private string ObtenerEstado(EstadosUsuario estado)
        {
            switch (estado)
            {
                case EstadosUsuario.Inactive:
                    return "INACTIVE";
                case EstadosUsuario.Active:
                    return "ACTIVE";
                default:
                    return "ACTIVE";
            }
        }

        /// <summary>
        /// Inserta un usuario.
        /// </summary>
        /// <param name="usuario">Contiene la información del usuario</param>
        public void InsertarUsuario(User usuario)
        {
            try
            {
                // Query de inserción
                string query = @"INSERT INTO Usuarios.usuario (nombreCompleto, username, password, estado)
                                 VALUES (@nombreCompleto, @username, @password, @estado)";

                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@nombreCompleto", usuario.Name);
                sqlCommand.Parameters.AddWithValue("@username", usuario.Username);
                sqlCommand.Parameters.AddWithValue("@password", usuario.Password);
                sqlCommand.Parameters.AddWithValue("@estado", ObtenerEstado(usuario.Status));

                // Ejecutar el comando de inserción
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // Cerrar la conexión
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Muestra todas los usuarios
        /// </summary>
        /// <returns>Un listado de los usuarios</returns>
        public List<User> MostrarUsuarios()
        {
            // Inicializar una lista vacía de usuarios
            List<User> usuarios = new List<User>();

            try
            {
                // Query de selección
                string query = @"SELECT * FROM Usuarios.usuario";

                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Obtiene los datos de los usuarios
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                        usuarios.Add(new User
                        {
                            ID = Convert.ToInt32(rdr["id"]),
                            Name = rdr["nombreCompleto"].ToString(),
                            Username = rdr["username"].ToString(),
                            Password = rdr["password"].ToString(),
                            Status = (EstadosUsuario)Convert.ToChar(rdr["estado"].ToString().Substring(0, 1))
                });
                }

                return usuarios;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // Cerrar la conexión
                sqlConnection.Close();
            }
        }     

        /// <summary>
        /// Modifica los datos de un usuario
        /// </summary>
        /// <param name="usuario">ID del usuario</param>
        public void ModificarUsuario(User usuario, int id)
        {
            try
            {
                // Query de actualización
                string query = @"UPDATE Usuarios.usuario
                                 SET nombreCompleto = @nombreCompleto, username = @username, password = @password, estado = @estado
                                 WHERE id = @id";

                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlCommand.Parameters.AddWithValue("@nombreCompleto", usuario.Name);
                sqlCommand.Parameters.AddWithValue("@username", usuario.Username);
                sqlCommand.Parameters.AddWithValue("@password", usuario.Password);
                sqlCommand.Parameters.AddWithValue("@estado", ObtenerEstado(usuario.Status));

                // Ejecutar el comando de actualización
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // Cerrar la conexión
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <param name="id">ID del usuario</param>
        public void EliminarUsuario(int id)
        {
            try
            {
                // Query de eliminación
                string query = @"DELETE FROM Usuarios.usuario
                                 WHERE id = @id";

                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer el valor del parámetro
                sqlCommand.Parameters.AddWithValue("@id", id);

                // Ejecutar el comando de eliminación
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // Cerrar la conexión
                sqlConnection.Close();
            }
        }
    }
}
