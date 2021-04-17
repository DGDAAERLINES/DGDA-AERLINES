using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregar los namespaces necesarios para conectarse a SQL Server
using System.Data.SqlClient;
using System.Configuration;


namespace DGDA_AIRLINES
{
    class Reservaciones
    {

        // Variables miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["DGDA_AIRLINES.Properties.Settings.DGDA_AIRLINES"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);


        public int ID { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaRegreso { get; set; }
        public string HoraRegreso { get; set; }
        public string HoraSalida { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }



        // Constructores
        public Reservaciones() { }


        public Reservaciones(DateTime fechaSalida, string horaSalida, DateTime fechaRegreso, string horaRegreso, string origen, string destino)
        {
            FechaSalida = fechaSalida;
            HoraSalida = horaSalida;
            FechaRegreso = fechaRegreso;
            HoraRegreso = horaRegreso;
            Origen = origen;
            Destino = destino;
        }

        /// <summary>
        /// Obtiene un vuelo por su id
        /// </summary>
        /// <param name="idvuelo">El id del vuelo</param>
        /// <returns>Los datos del vuelon</returns>
        public Reservaciones BuscarVuelo(int idvuelo)
        {
            Reservaciones Vuelo = new Reservaciones();


            try
            {
                // Query de búsqueda
                string query = @"SELECT * FROM Aerlines.Vuelo
                                 WHERE idvuelo = @idvuelo";

                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer el valor del parámetro
                sqlCommand.Parameters.AddWithValue("@idvuelo", ID);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Vuelo.ID = Convert.ToInt32(rdr["idvuelo"]);
                        Vuelo.FechaSalida = Convert.ToDateTime(rdr["fechaSalida"]);
                        Vuelo.HoraSalida = rdr["HoraSalida"].ToString();
                        Vuelo.FechaRegreso = Convert.ToDateTime(rdr["fechaRegreso"]);
                        Vuelo.HoraRegreso = rdr["HoraRegreso"].ToString();
                        Vuelo.Origen = rdr["Origen"].ToString();
                        Vuelo.Destino = rdr["Destino"].ToString();


                    }
                }

                return Vuelo;
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
        /// Inserta un vuelo.
        /// </summary>
        /// <param name="vuelo">La información del vuelo</param>
        public void Crearvuelo(Reservaciones vuelo)
        {
            try
            {

                // Crear el comando SQL
                string query = @"insert into Aerlines.Vuelo (fechaSalida,HoraSalida, fechaRegreso,HoraRegreso, Origen, Destino)
                    values(@fechaSalida,@HoraSalida, @fechaRegreso,@HoraRegreso, @Origen, @Destino)";

                //  sqlCommand.CommandType = CommandType.Text
                // Establecer la conexión
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@fechaSalida", vuelo.FechaSalida.ToString("yyyy-MM-dd "));
                sqlCommand.Parameters.AddWithValue("@HoraSalida", vuelo.HoraSalida);
                sqlCommand.Parameters.AddWithValue("@fechaRegreso", vuelo.FechaRegreso.ToString("yyyy-MM-dd "));
                sqlCommand.Parameters.AddWithValue("HoraRegreso", vuelo.HoraRegreso);
                sqlCommand.Parameters.AddWithValue("@Origen", vuelo.Origen);
                sqlCommand.Parameters.AddWithValue("@Destino", vuelo.Destino);

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
        /// Muestra todas los Vuelos
        /// </summary>
        /// <returns>Un listado de Vuelos</returns>
        public List<Reservaciones> Mostrarvuelos()
        {
            // Inicializar una lista vacía de vuelos
            List<Reservaciones> Vuelos = new List<Reservaciones>();



            try
            {
                // Query de selección
                string query = @"select * from Aerlines.Vuelo  ";

                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);


                // Obtener los datos de los vuelos
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {


                    while (rdr.Read())

                        Vuelos.Add(new Reservaciones
                        {
                            ID = Convert.ToInt32(rdr["idvuelo"]),
                            FechaSalida = Convert.ToDateTime(rdr["fechaSalida"].ToString()),
                            HoraSalida = rdr["HoraSalida"].ToString(),
                            FechaRegreso = Convert.ToDateTime(rdr["fechaRegreso"].ToString()),
                            HoraRegreso = rdr["HoraRegreso"].ToString(),
                            Destino = rdr["Destino"].ToString(),
                            Origen = rdr["Origen"].ToString(),


                        });
                }

                return Vuelos;
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
        /// Modifica los datos del vuelo
        /// </summary>
        /// <param name="Vuelo">El id del vuelo</param
        public void Modificarvuelo(Reservaciones Vuelo, int idvuelo)
        {
            try
            {
                // Query de actualización
                string query = @"UPDATE Aerlines.Vuelo
                               SET fechaSalida= @fechaSalida, HoraSalida = @HoraSalida, fechaRegreso = @fechaRegreso,HoraRegreso = @HoraRegreso, Origen=@Origen, Destino = @Destino
                               WHERE idVuelo = @idVuelo";

                // Establecer la conexión
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@idvuelo", idvuelo);
                sqlCommand.Parameters.AddWithValue("@fechaSalida", Vuelo.FechaSalida.ToString("yyyy-MM-dd "));
                sqlCommand.Parameters.AddWithValue("@HoraSalida", Vuelo.HoraSalida);
                sqlCommand.Parameters.AddWithValue("@fechaRegreso", Vuelo.FechaRegreso.ToString("yyyy-MM-dd "));
                sqlCommand.Parameters.AddWithValue("@HoraRegreso", Vuelo.HoraRegreso);
                sqlCommand.Parameters.AddWithValue("@Origen", Vuelo.Origen);
                sqlCommand.Parameters.AddWithValue("@Destino", Vuelo.Destino);

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
