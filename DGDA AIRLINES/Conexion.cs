using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CRUD_Pasajeros
{
    class Conexion
    {
        //Variables miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["DGDA_AIRLINES.Properties.Settings.DGDA_AIRLINES"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        //Propiedades
        public object DataSource { get; set; }
        public int IdPasajero { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public int IdPais { get; set; }

        public string Sexo { get; set; }

        public string Edad { get; set; }

        public string Telefono { get; set; }

        public Conexion() { }

        public Conexion(int idPasajero, string nombre, string apellido, int idPais, string sexo, string edad, string telefono)
        {
            IdPasajero = idPasajero;
            Nombre = nombre;
            Apellido = apellido;
            IdPais = idPais;
            Sexo = sexo;
            Edad = edad;
            Telefono = telefono;
        }

        public List<Conexion> MostrarPasajeros()
        {
            // Inicializar una lista vacía de habitaciones
            List<Conexion> pasajeros = new List<Conexion>();

            try
            {

                // Query de selección
                string query = @"select idPasajero, nombre, apellido, idPais, sexo, edad, telefono
                               from Aerlines.Pasajero";

                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Obtener los datos de los pasajeros
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                        pasajeros.Add(new Conexion
                        {
                            IdPasajero = Convert.ToInt32(rdr["idPasajero"]),
                            Nombre = rdr["nombre"].ToString(),
                            Apellido = rdr["apellido"].ToString(),
                            IdPais = Convert.ToInt32(rdr["idPais"]),
                            Sexo = rdr["sexo"].ToString(),
                            Edad = rdr["edad"].ToString(),
                            Telefono = rdr["telefono"].ToString()
                        });
                }

                return pasajeros;
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


        public void InsertarPasajero(Conexion conexion)
        {
            try
            {
                string query = @"insert into Aerlines.Pasajero(idPasajero, nombre, apellido, idPais, sexo, edad, telefono)
	                           values(@id, @nombre, @apellido, @idPais, @sexo, @edad, @telefono)";

                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@id", conexion.IdPasajero);
                sqlCommand.Parameters.AddWithValue("@nombre", conexion.Nombre);
                sqlCommand.Parameters.AddWithValue("@apellido", conexion.Apellido);
                sqlCommand.Parameters.AddWithValue("@idPais", conexion.IdPais);
                sqlCommand.Parameters.AddWithValue("@sexo", conexion.Sexo);
                sqlCommand.Parameters.AddWithValue("@edad", conexion.Edad);
                sqlCommand.Parameters.AddWithValue("@telefono", conexion.Telefono);

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

        public void ModificarPasajero(Conexion conexion)
        {
            try
            {

                string query = @"update Aerlines.Pasajero
	                            set nombre = @nombre, apellido = @apellido, idPais = @idPais, sexo = @sexo, edad = @edad, telefono = @telefono
	                            where idPasajero = @id";

                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@id", conexion.IdPasajero);
                sqlCommand.Parameters.AddWithValue("@nombre", conexion.Nombre);
                sqlCommand.Parameters.AddWithValue("@apellido", conexion.Apellido);
                sqlCommand.Parameters.AddWithValue("@idPais", conexion.IdPais);
                sqlCommand.Parameters.AddWithValue("@sexo", conexion.Sexo);
                sqlCommand.Parameters.AddWithValue("@edad", conexion.Edad);
                sqlCommand.Parameters.AddWithValue("@telefono", conexion.Telefono);

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
        public void EliminarPasajero(int id, string nombre, string apellido, int idPais, string sexo, string edad, string telefono)
        {
            try
            {

                string query = @"update Aerlines.Pasajero
	                            set nombre = @nombre, apellido = @apellido, idPais = @idPais, sexo = @sexo, edad = @edad, telefono = @telefono
	                            where idPasajero = @id";

                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlCommand.Parameters.AddWithValue("@nombre", nombre);
                sqlCommand.Parameters.AddWithValue("@apellido", apellido);
                sqlCommand.Parameters.AddWithValue("@idPais", idPais);
                sqlCommand.Parameters.AddWithValue("@sexo", sexo);
                sqlCommand.Parameters.AddWithValue("@edad", edad);
                sqlCommand.Parameters.AddWithValue("@telefono", telefono);

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
    }
}
