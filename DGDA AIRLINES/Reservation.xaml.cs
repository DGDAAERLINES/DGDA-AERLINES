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
        private Reservaciones Vuelo = new Reservaciones();
        private List<Reservaciones> Vuelos;
        private SqlConnection sqlConnection;



        // Constructores
        public Reservation()
        {
            InitializeComponent();

            // Realizar la conexión con el servidor de base de datos (SQL Server Express)
            string connectionString = ConfigurationManager.ConnectionStrings["DGDA_AIRLINES.Properties.Settings.DGDA_AIRLINES"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
            // Metodo de mostrar paises
            MostrarAeropuerto();
            MostrarPaisMundo();
            Obtenervuelos();
     




        }
        // Obtiene de la base de datos una lista de los vuelos registrados
        private void Obtenervuelos()
        {

            Vuelos = Vuelo.Mostrarvuelos();
            DataG.ItemsSource = Vuelos;
        }

        private void LimpiarFormulario()
        {
            txtID.Text = string.Empty;
            cmbOrigen.SelectedValue = null;
            cmbDestino.SelectedValue = null;
            dtSalida.SelectedDate = null;
            dtRegreso.SelectedDate = null;
            HSalida.SelectedTime = null;
            HRegreso.SelectedTime = null;

        }

        private bool VerificarValores()
        {
            if (cmbOrigen.SelectedItem == null || (cmbDestino.SelectedItem == null))

            {
                MessageBox.Show("Please fill all fields");
                return false;
            }


            return true;
        }
        private bool VerificarValoresCalendar()
        {

            if (dtRegreso.SelectedDate < dtSalida.SelectedDate)
            {
                MessageBox.Show("The return date cannot be less than the departure date.");
                return false;
            }
            else if (dtSalida.SelectedDate == null && (dtRegreso.SelectedDate == null))
            {
                MessageBox.Show("Please fill all fields");
                return false;
            }


            return true;
        }

        private void ObtenerValoresFormulario()

        {
            
            Vuelo.Origen = cmbOrigen.Text;
            Vuelo.Destino = cmbDestino.Text;
            Vuelo.FechaRegreso = Convert.ToDateTime(dtRegreso.DisplayDate);
            Vuelo.FechaSalida = Convert.ToDateTime(dtSalida.DisplayDate);
             Vuelo.HoraSalida = HSalida.Text;
            Vuelo.HoraRegreso = HRegreso.Text;


        }

        private void Valorestxt()
        {
            Reservaciones objvuelo = new Reservaciones();
            objvuelo = (Reservaciones)DataG.SelectedItem;
            txtID.Text = objvuelo.ID.ToString();
        }

        private void ValoresFormularioDesdeObjeto()
        {
            // Crea un objeto de tipo vuelo que captura los valores del DataGrid
            Reservaciones objvuelo = new Reservaciones();
            objvuelo = (Reservaciones)DataG.SelectedItem;
            txtID.Text = objvuelo.ID.ToString();
            cmbDestino.SelectedItem = objvuelo.Destino.ToString();
            cmbOrigen.SelectedItem = objvuelo.Origen.ToString();
            dtRegreso.SelectedDate = objvuelo.FechaRegreso;
            dtSalida.SelectedDate = objvuelo.FechaSalida;
            HSalida.Text = objvuelo.HoraSalida;
           HRegreso.Text= objvuelo.HoraRegreso;



        }
  


        private void MostrarAeropuerto()
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
                    DataTable tablaAeropuerto = new DataTable();

                    // LLenar el objeto de tipo DataTable con los valores proveniente del SqlDataAdapter
                    sqlDataAdapter.Fill(tablaAeropuerto);

                    cmbOrigen.DisplayMemberPath = "Nombre";
                    cmbOrigen.SelectedValuePath = "idAeropuertoHN"; ;

                    cmbOrigen.ItemsSource = tablaAeropuerto.DefaultView;
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
                string query = "select *  from Aerlines.Pais";

                // SqlDataAdapter es una interfaz entre las tablas de la base de datos
                // y los objetos utilizables en C#
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);

                // Utilizar el SqlDataAdapter
                using (sqlDataAdapter)
                {
                    // Objeto de C# que refleje la estructura de una tabla
                    DataTable tablaPaises = new DataTable();

                    // LLenar el objeto de tipo DataTable con los valores proveniente del SqlDataAdapter
                    sqlDataAdapter.Fill(tablaPaises);


                    cmbDestino.DisplayMemberPath = "nombre";
                    cmbDestino.SelectedValuePath = "id"; ;
                    cmbDestino.ItemsSource = tablaPaises.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnInsertar_Click(object sender, RoutedEventArgs e)
        {

            // Verificar que se ingresaron los valores requeridos
            if (VerificarValores() && VerificarValoresCalendar())
            {

                try
                {
                    //Metodo que manda a llamar el dataGrid
                    ObtenerValoresFormulario();
                    Vuelo.Crearvuelo(Vuelo);
                    MessageBox.Show("Flight inserted correctly!");

              

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data Insert Error!");
                    Console.WriteLine(ex.Message);
                }
                finally
                {

                    LimpiarFormulario();
                    Obtenervuelos();
                }

            }
        }

        private void OcultarBotones(Visibility ocultar)
        {
            btnInsertar.Visibility = ocultar;
            btnActualizar.Visibility = ocultar;
            btnRegresar.Visibility = ocultar;
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (DataG.SelectedValue == null)
                MessageBox.Show("Please select a flight from the list");
            else
            {
                Reservaciones objvuelo = new Reservaciones();
                objvuelo = (Reservaciones)DataG.SelectedItem;
                int idvuelo = objvuelo.ID;

                try
                {
                    // Obtener la información del vuelo
                    Vuelo = Vuelo.BuscarVuelo(idvuelo);

                    // Llenar los valores del formulario
                    ValoresFormularioDesdeObjeto();
                    OcultarBotones(Visibility.Hidden);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while modifying the flight...");
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Obtenervuelos();
                }
            }
        }

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            OcultarBotones(Visibility.Visible);
            LimpiarFormulario();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                // Obtener los valores para la habitación desde el formulario
                ObtenerValoresFormulario();

                // Actualizar los valores en la base de datos
                Vuelo.Modificarvuelo(Vuelo, Convert.ToInt32(txtID.Text));

                // Mensaje de actualización realizada
                MessageBox.Show("Sucessfully Fligth update");

                OcultarBotones(Visibility.Visible);

                LimpiarFormulario();
                ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when updating the flight...");
                Console.WriteLine(ex.Message);
            }
            finally
            {

                // Actualizar el datagrid de vuelos
                Obtenervuelos();
            }

        }

     

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            // Retornar el usuario al formulario de Menu
            Menu Menu = new Menu();
            Menu.Show();
            Close();
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
          
            Reservaciones objvuelo = new Reservaciones();
            objvuelo = (Reservaciones)DataG.SelectedItem;
            int idvuelo = objvuelo.ID;

            try
            {
                // Obtener la información del vuelo
                Vuelo = Vuelo.BuscarVuelo(idvuelo);

                // Llenar los valores del formulario
                Valorestxt();
           
                // Retornar el usuario al formulario de Precios
                Prices prices = new Prices(Convert.ToInt32(txtID.Text));
                prices.Show();
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while modifying the flight...");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Obtenervuelos();
            }
        }
    }

       
    
}
