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
    class Price
    {
        // Variables miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["DGDA_AIRLINES.Properties.Settings.DGDA_AIRLINES"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);
        public int destinoKey;
        public string origen;
        public string destino;

        // Propiedades
        public int idClase { get; set; }
        public int idVuelo { get; set; }
        public double Precio { get; set; }

        // Constructores 
        public Price() { }

        public Price(int idclase, int idvuelo, double precio)
        {
            idClase = idclase;
            idVuelo = idvuelo;
            Precio = precio;
        }

        // Métodos
        /// <summary>
        /// Inserta el precio
        /// </summary>
        /// <param name="precio">Contiene los datos relacionados con el precio</param>
        public void InsertarPrecio(Price precio)
        {
            try
            {
                // Query de inserción
                string query = @"INSERT INTO Aerlines.DetallePrecio (idClase, idVuelo, Precio)
                                 VALUES (@idClase, @idVuelo, @precio)";

                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@idClase", precio.idClase);
                sqlCommand.Parameters.AddWithValue("@idVuelo", precio.idVuelo);
                sqlCommand.Parameters.AddWithValue("@precio", precio.Precio);


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

        // Métodos
        /// <summary>
        /// Obtiene la cadena de texto que contiene el destino del vuelo
        /// </summary>
        /// <param name="id">Contiene el id del vuelo del que se desea obtener el destino</param>
        public string ObtenerDestino(int id)
        {
            try
            {
                // Query de selección
                string query = @"SELECT V.destino FROM Aerlines.Vuelo V WHERE V.idVuelo = @idVuelo";

                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@idVuelo", id);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Obtener el destino desde la base de datos
                        destino = rdr["destino"].ToString();
                    }
                }

                // Retornar el destino
                return destino;
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
        /// Obtiene el número entero que contiene el key del origen
        /// </summary>
        /// <param name="id">Contiene el id del vuelo del que se desea obtener el key de destino</param>
        public int ObtenerKeyDestino(int id)
        {
            try
            {
                // Query de selección
                string query = @"SELECT destino from Aerlines.Vuelo WHERE idVuelo = @idVuelo";

                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@idVuelo", id);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Obtener el key del destino desde la base de datos
                        destinoKey = Convert.ToInt32(rdr["destino"]);
                    }
                }

                // Retornar el key de destino
                return destinoKey;
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
        /// Obtiene la cadena de texto que contiene el origen del vuelo
        /// </summary>
        /// <param name="id">Contiene el id del vuelo del que se desea obtener el origen</param>
        public string ObtenerOrigen(int id)
        {
            try
            {
                // Query de selección
                string query = @"SELECT V.origen FROM Aerlines.Vuelo V WHERE V.idVuelo = @idVuelo";

                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@idVuelo", id);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Obtener el origen desde la base de datos
                        origen = rdr["origen"].ToString();
                    }
                }

                // Retornar el origen
                return origen;
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

