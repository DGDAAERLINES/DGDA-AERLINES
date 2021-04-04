using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
// Agregar los namespaces necesarios para conectarse a SQL Server
using System.Data;
using System.Data.SqlClient;
using System.Configuration;




namespace DGDA_AIRLINES
{
    /// <summary>
    /// Lógica de interacción para Reservation.xaml
    /// </summary>
    public partial class Reservation : Window
    {

        // Variables miembro
        private SqlConnection sqlConnection;
        // Variables miembro
        private Reservations Vuelo = new Reservations();
        private List<Reservations> Vuelos;

        // Constructores
        public Reservation()
        {
            InitializeComponent();

            // Realizar la conexión con el servidor de base de datos (SQL Server Express)
            string connectionString = ConfigurationManager.ConnectionStrings["DGDA_AIRLINES.Properties.Settings.DGDA_AIRLINES"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);

            // Metodo de mostrar paises
            MostrarPais();
            MostrarPaisMundo();
            llenarDataG();


           // cmbopcion.Items.Add("solo ida");
            //cmbopcion.Items.Add("ida y regreso");


        }

        
        private void MostrarPais()
        {
            try
            {
                // El query de consulta a la tabla de la base de datos
                string query = "SELECT * FROM Aerlines.AeropuertoHN";

                // SqlDataAdapter es una interfaz entre las tablas de la base de datos
                // y los objetos utilizables en C#
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);

                // Utilizar el SqlDataAdapter
                using (sqlDataAdapter)
                {
                    // Objeto de C# que refleje la estructura de una tabla
                    DataTable tablaPais = new DataTable();

                    // LLenar el objeto de tipo DataTable con los valores proveniente del SqlDataAdapter
                    sqlDataAdapter.Fill(tablaPais);


                    cmbOrigen.DisplayMemberPath = "Nombre";
                    //cmbDestino.DisplayMemberPath = "nombre";


                    // cmbOrigen.SelectedValuePath = "idPais";
                    /// cmbDestino.SelectedValuePath = "idPais";

                    cmbOrigen.SelectedValuePath = "idAeropuertoHN";
                    // cmbDestino.DisplayMemberPath = "nombre";

                    // ¿Quién es la referencia para los datos del ListBox? (populate)
                    cmbOrigen.ItemsSource = tablaPais.DefaultView;
                    // cmbDestino.ItemsSource = tablaPais.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void MostrarPaisMundo()
        {
            try
            {
                // El query de consulta a la tabla de la base de datos
                string query = @"select *  from Aerlines.Pais";

                // SqlDataAdapter es una interfaz entre las tablas de la base de datos
                // y los objetos utilizables en C#
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);

                // Utilizar el SqlDataAdapter
                using (sqlDataAdapter)
                {
                    // Objeto de C# que refleje la estructura de una tabla
                    DataTable tablaAeropuerto = new DataTable();

                    // LLenar el objeto de tipo DataTable con los valores proveniente del SqlDataAdapter
                    sqlDataAdapter.Fill(tablaAeropuerto);


                    cmbDestino.DisplayMemberPath = "nombre";
                    //cmbDestino.DisplayMemberPath = "nombre";


                    // cmbOrigen.SelectedValuePath = "idPais";
                    /// cmbDestino.SelectedValuePath = "idPais";

                    cmbDestino.SelectedValuePath = "id";
                    // cmbDestino.DisplayMemberPath = "nombre";

                    // ¿Quién es la referencia para los datos del ListBox? (populate)
                    // cmbOrigen.ItemsSource = tablaAeropuerto.DefaultView;
                    cmbDestino.ItemsSource = tablaAeropuerto.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void ObtenerValoresFormulario()
        {

            Vuelo.Id = cmbDestino.SelectedIndex;
            Vuelo.IdAeropuertoHN = cmbOrigen.SelectedIndex;
            Vuelo.FechaLLegada = dtRegreso.DisplayDate;
            Vuelo.FechaSalida = dtSalida.DisplayDate;
            
            Vuelo.IdVuelo = Convert.ToInt32(DataG.SelectedValue);
        }
        private bool VerificarValores()
        {
            if (cmbOrigen.SelectedItem == null || (cmbDestino.SelectedItem == null))

            {
                MessageBox.Show("Debe de llenar todos los campos");
                return false;
            }


            return true;
        }
        private bool VerificarValoresCalendar()
        {

            if (dtRegreso.SelectedDate < dtSalida.SelectedDate)
            {
                MessageBox.Show("La fecha de regreso no puede ser menor que la fecha de salida.");
                return false;
            }
            else if (dtSalida.SelectedDate == null && (dtRegreso.SelectedDate == null))
            {
                MessageBox.Show("Por favor llene todos los campos");
                return false;
            }

            else if (chEconomica.IsChecked == null || chEjecutiva.IsChecked == null)
            {
                MessageBox.Show("por favor elija una clase ");
                return false;
            }
            

            return true;
        }




        

        private void LimpiarFormulario()
        {
           
            cmbOrigen.SelectedValue = null;
            cmbDestino.SelectedValue = null;
            dtSalida.SelectedDate = null;
            dtRegreso.SelectedDate = null;
        }

        private void chEjecutiva_Checked(object sender, RoutedEventArgs e)
        {
            if (chEjecutiva.IsChecked == true)
            {
                chEconomica.IsChecked = false;
            }
            else
            {
                chEconomica.IsChecked = true;
            }
        }

        private void chEconomica_Checked(object sender, RoutedEventArgs e)
        {
            if (chEconomica.IsChecked == true)
            {
                chEjecutiva.IsChecked = false;
            }
            else
            {
                chEjecutiva.IsChecked = true;
            }
        }
        

        private void llenarDataG()

        {
            
            SqlCommand sqlCommand = new SqlCommand("MostrarVuelos", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            sqlConnection.Open();
            SqlDataReader sdr = sqlCommand.ExecuteReader();
            dt.Load(sdr);
            sqlConnection.Close();
            DataG.ItemsSource = dt.DefaultView;


        }

        private void btninsertar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarValores() && VerificarValoresCalendar())
            {
                try
                {


                    // Obtener los valores para la habitación
                    ObtenerValoresFormulario();

                  
                    Vuelo.Crearvuelo(Vuelo);
                    llenarDataG();
                   

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar el vuelo...");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    LimpiarFormulario();

                }
            }
        }

        private void ValoresFormularioDesdeObjeto()
        {
            cmbDestino.SelectedValue = Vuelo.IdAeropuertoHN;
            cmbOrigen.SelectedValue = Vuelo.Id;
            dtRegreso.DisplayDate = Vuelo.FechaLLegada;
            dtSalida.DisplayDate = Vuelo.FechaSalida;

            
        }
        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            

            if (DataG.SelectedValue == null)
                MessageBox.Show("Por favor selecciona ");
            else
            {
                try
                {
                    // Obtener la información  del vuelo
                    Vuelo = Vuelo.BuscarVuelo(Convert.ToInt32(DataG.SelectedValue));

                    // Llenar los valores del formulario
                    ValoresFormularioDesdeObjeto();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de modificar el vuelo...");
                    MessageBox.Show(ex.Message);
                }
            }
        }


    }
}
