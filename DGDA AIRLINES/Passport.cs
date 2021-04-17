using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregar los namespaces requeridos
using System.Data.SqlClient;
using System.Configuration;

namespace DGDA_AIRLINES
{
    //Variable que contiene los valores del tipo del pasaporte
    public enum Type
    {
        Firstime = 'P',
        Renovation = 'R',
    }
    class Passport
    {

        // Variables miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["DGDA_AIRLINES.Properties.Settings.DGDA_AIRLINES"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);
        // Propiedades
        // Propiedades ID pasaporte
        public int ID { get; set; }
        // Propiedades tipo de pasaporte
        public Type Type { get; set; }
        // Propiedades pais emisor<
        public string IssuingState { get; set; }
        // Propiedades numero de pasaporte
        public string PassportNo { get; set; }
        // Propiedades apellidos
        public string Surname { get; set; }
        // Propiedades nombres
        public string GivenName { get; set; }
        // Propiedades fecha de nacimiento
        public DateTime DateofBirth { get; set; }
        // Propiedades nacionalidad
        public string Nationality { get; set; }
        // Propiedades genero
        public string Sex { get; set; }
        // Propiedades numero de identidad
        public string IDNo { get; set; }
        // Propiedades lugar de nacimiento
        public string PlaceofBirth { get; set; }
        // Propiedades fecha de emision del pasaporte
        public DateTime DateofIssue { get; set; }
        // Propiedades fecha en que expira el pasaporte
        public DateTime DateofExpiry { get; set; }
        // Propiedades aotoridad de emision del pasaporte
        public string AuthorityofIssue { get; set; }

        // Constructores
        public Passport() { }

        public Passport(Type Abrebiaturat, string Abrebiaturap, string PasaporteNo, string Apellidos, string Nombres, DateTime FechaDeNacimiento, string Nacionalidad, string Abrebiaturag, string NIdentida, string Pais, DateTime FechaDeEmision, DateTime FechaDeVencimiento, string AutoridadEmisora)
        {
            //valores del construtor
            Type = Abrebiaturat;
            IssuingState = Abrebiaturap;
            PassportNo = PasaporteNo;
            Surname = Apellidos;
            GivenName = Nombres;
            DateofBirth = FechaDeNacimiento;
            Nationality = Nacionalidad;
            Sex = Abrebiaturag;
            IDNo = NIdentida;
            PlaceofBirth = Pais;
            DateofIssue = FechaDeEmision;
            DateofExpiry = FechaDeVencimiento;
            AuthorityofIssue = AutoridadEmisora;
        }

        // Métodos
        /// <summary>
        /// Obtiene un pasaporte por su ID
        /// </summary>
        /// <param name="IdPasaporte">El id del pasaporte</param>
        /// <returns>Los datos del pasaporte</returns>
        public Passport BuscarPasaporteID(int IdPasaporte)
        {
            // Crear el objeto que almacena la información de los resultados
            Passport Pasaporte = new Passport();

            try
            {
                // Query de selección
                string query = @"SELECT * FROM Pasaportes.Pasaporte
                                 WHERE IdPasaporte = @IdPasaporte";


                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@IdPasaporte", ID);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // Obtener los valores del pasaporte si la consulta retorna valores
                        Pasaporte.ID = Convert.ToInt32(rdr["IdPasaporte"]);
                        Pasaporte.Type = (Type)Convert.ToChar(rdr["Abrebiaturat"].ToString().Substring(0, 1));
                        Pasaporte.IssuingState = rdr["Abrebiaturap"].ToString();
                        Pasaporte.PassportNo = rdr["PasaporteNo"].ToString();
                        Pasaporte.Surname = rdr["Apellidos"].ToString();
                        Pasaporte.GivenName = rdr["Nombres"].ToString();
                        Pasaporte.DateofBirth = Convert.ToDateTime(rdr["FechaDeNacimiento"]);
                        Pasaporte.Nationality = rdr["Nacionalidad"].ToString();
                        Pasaporte.Sex = rdr["Abrebiaturag"].ToString();
                        Pasaporte.IDNo = rdr["NIdentida"].ToString();
                        Pasaporte.PlaceofBirth = rdr["Pais"].ToString();
                        Pasaporte.DateofIssue = Convert.ToDateTime(rdr["FechaDeEmision"]);
                        Pasaporte.DateofExpiry = Convert.ToDateTime(rdr["FechaDeVencimiento"]);
                        Pasaporte.AuthorityofIssue = rdr["AutoridadEmisora"].ToString();
                    }
                }

                // Retornar el pasaporte con los valores
                return Pasaporte;
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
        /// Retorna el tipo del pasaporte desde el enum 'Type'
        /// </summary>
        /// <param name="type">El valor dentro del enum</param>
        /// <returns>Tipo válido dentro de la base de datos</returns>
        private string ObtenerTipo(Type type)
        {
            //casos validos del combobox
            switch (Type)
            {
                case Type.Firstime:
                    return "P";
                case Type.Renovation:
                    return "R";
                default:
                    return "P";
            }
        }
        /// <summary>
        /// Inserta un pasaporte.
        /// </summary>
        /// <param name="pasaporte">Contiene la información del pasaporte</param>
        public void InsertarPasaporte(Passport Pasaporte)
        {
            try
            {
                // Query de inserción
                string query = @"INSERT INTO Pasaportes.Pasaporte (Abrebiaturat, Abrebiaturap, PasaporteNo, Apellidos, Nombres, FechaDeNacimiento, Nacionalidad, Abrebiaturag, NIdentida, Pais, FechaDeEmision, FechaDeVencimiento, AutoridadEmisora)
                                                        VALUES (@Abrebiaturat, @Abrebiaturap, @PasaporteNo, @Apellidos, @Nombres, @FechaDeNacimiento, @Nacionalidad, @Abrebiaturag, @NIdentida, @Pais, @FechaDeEmision, @FechaDeVencimiento, @AutoridadEmisora)";

                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@Abrebiaturat", ObtenerTipo(Pasaporte.Type));
                sqlCommand.Parameters.AddWithValue("@Abrebiaturap", Pasaporte.IssuingState);
                sqlCommand.Parameters.AddWithValue("@PasaporteNo", Pasaporte.PassportNo);
                sqlCommand.Parameters.AddWithValue("@Apellidos", Pasaporte.Surname);
                sqlCommand.Parameters.AddWithValue("@Nombres", Pasaporte.GivenName);
                sqlCommand.Parameters.AddWithValue("@FechaDeNacimiento", Pasaporte.DateofBirth);
                sqlCommand.Parameters.AddWithValue("@Nacionalidad", Pasaporte.Nationality);
                sqlCommand.Parameters.AddWithValue("@Abrebiaturag", Pasaporte.Sex);
                sqlCommand.Parameters.AddWithValue("@NIdentida", Pasaporte.IDNo);
                sqlCommand.Parameters.AddWithValue("@Pais", Pasaporte.PlaceofBirth);
                sqlCommand.Parameters.AddWithValue("@FechaDeEmision", Pasaporte.DateofIssue);
                sqlCommand.Parameters.AddWithValue("@FechaDeVencimiento", Pasaporte.DateofExpiry);
                sqlCommand.Parameters.AddWithValue("@AutoridadEmisora", Pasaporte.AuthorityofIssue);

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
        /// Muestra todas los pasaportes
        /// </summary>
        /// <returns>Un listado de los pasaportes</returns>
        public List<Passport> MostrarPasaportes()
        {
            // Inicializar una lista vacía de Pasaportes
            List<Passport> Pasaportes = new List<Passport>();


            try
            {
                // Query de selección
                string query = @"SELECT * FROM Pasaportes.Pasaporte";

                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Obtiene los datos de los pasaportes
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                        Pasaportes.Add(new Passport
                        {
                            ID = Convert.ToInt32(rdr["IdPasaporte"]),
                            Type = (Type)Convert.ToChar(rdr["Abrebiaturat"].ToString().Substring(0, 1)),
                            IssuingState = rdr["Abrebiaturap"].ToString(),
                            PassportNo = rdr["PasaporteNo"].ToString(),
                            Surname = rdr["Apellidos"].ToString(),
                            GivenName = rdr["Nombres"].ToString(),
                            DateofBirth = Convert.ToDateTime(rdr["FechaDeNacimiento"]),
                            Nationality = rdr["Nacionalidad"].ToString(),
                            Sex = rdr["Abrebiaturag"].ToString(),
                            IDNo = rdr["NIdentida"].ToString(),
                            PlaceofBirth = rdr["Pais"].ToString(),
                            DateofIssue = Convert.ToDateTime(rdr["FechaDeEmision"]),
                            DateofExpiry = Convert.ToDateTime(rdr["FechaDeVencimiento"]),
                            AuthorityofIssue = rdr["AutoridadEmisora"].ToString()
                        });
                }
                //retorna los pasaportes
                return Pasaportes;
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
        /// Modifica los datos de un pasaporte
        /// </summary>
        /// <param name="Pasaporte">ID del pasaporte</param>
        public void ModificarPasaporte(Passport Pasaporte, int IdPasaporte)
        {
            try
            {
                // Query de actualización
                string query = @"UPDATE Pasaportes.Pasaporte
                                 SET Abrebiaturat = @Abrebiaturat, Abrebiaturap = @Abrebiaturap, PasaporteNo = @PasaporteNo, Apellidos = @Apellidos, Nombres = @Nombres, FechaDeNacimiento = @FechaDeNacimiento, Nacionalidad = @Nacionalidad, Abrebiaturag = @Abrebiaturag, NIdentida = @NIdentida, Pais = @Pais, FechaDeEmision = @FechaDeEmision, FechaDeVencimiento = @FechaDeVencimiento, AutoridadEmisora = @AutoridadEmisora
                                 WHERE IdPasaporte = @IdPasaporte";

                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@IdPasaporte", IdPasaporte);
                sqlCommand.Parameters.AddWithValue("@Abrebiaturat", ObtenerTipo(Pasaporte.Type));
                sqlCommand.Parameters.AddWithValue("@Abrebiaturap", Pasaporte.IssuingState);
                sqlCommand.Parameters.AddWithValue("@PasaporteNo", Pasaporte.PassportNo);
                sqlCommand.Parameters.AddWithValue("@Apellidos", Pasaporte.Surname);
                sqlCommand.Parameters.AddWithValue("@Nombres", Pasaporte.GivenName);
                sqlCommand.Parameters.AddWithValue("@FechaDeNacimiento", Pasaporte.DateofBirth);
                sqlCommand.Parameters.AddWithValue("@Nacionalidad", Pasaporte.Nationality);
                sqlCommand.Parameters.AddWithValue("@Abrebiaturag", Pasaporte.Sex);
                sqlCommand.Parameters.AddWithValue("@NIdentida", Pasaporte.IDNo);
                sqlCommand.Parameters.AddWithValue("@Pais", Pasaporte.PlaceofBirth);
                sqlCommand.Parameters.AddWithValue("@FechaDeEmision", Pasaporte.DateofIssue);
                sqlCommand.Parameters.AddWithValue("@FechaDeVencimiento", Pasaporte.DateofExpiry);
                sqlCommand.Parameters.AddWithValue("@AutoridadEmisora", Pasaporte.AuthorityofIssue);

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
        /// Elimina un pasaporte
        /// </summary>
        /// <param name="IdPasaporte">ID del pasaporte</param>
        public void EliminarPasaporte(int IdPasaporte)
        {
            try
            {
                // Query de eliminación
                string query = @"DELETE FROM Pasaportes.Pasaporte
                                 WHERE IdPasaporte = @IdPasaporte";

                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer el valor del parámetro
                sqlCommand.Parameters.AddWithValue("@IdPasaporte", IdPasaporte);

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
